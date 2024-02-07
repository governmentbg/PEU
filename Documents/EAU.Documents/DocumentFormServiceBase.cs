using AutoMapper;
using CNSys;
using CNSys.Xml;
using CNSys.Xml.Schema;
using EAU.Documents.Common;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.XSDSchemas;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using EAU.Signing;
using EAU.Signing.Models;
using FluentValidation;
using FluentValidation.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace EAU.Documents
{
    public abstract class DocumentFormServiceBase<TDomain, TView> :
        IDocumentFormInitializationService,
        IDocumentFormService,
        IDocumentFormValidationService,
        IDocumentFormPrintService
        where TDomain : IDocumentForm
        where TView : DocumentFormVMBase
    {
        private readonly IMapper Mapper;
        private readonly IServiceProvider ServiceProvider;

        public DocumentFormServiceBase(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Mapper = GetService<IMapper>();
        }

        protected T GetService<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }

        protected abstract string DocumentTypeUri { get; }

        /// <summary>
        /// Обект за данни за XSL трансформация.
        /// </summary>
        protected abstract PrintPreviewData PrintPreviewData { get; }

        #region IDocumentInitializationService

        Task<OperationResult> IDocumentFormInitializationService.InitializeDocumentFormAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            request.AdditionalData["hasPrintPreview"] = (PrintPreviewData != null).ToString();

            return InitializeDocumentFormInternalAsync(request, cancellationToken);
        }

        #endregion

        #region IDocumentFormService

        public void SerializeDomainForm(XmlWriter writer, object domainForm)
        {
            var serializer = new XmlSerializer(typeof(TDomain));
            serializer.Serialize(writer, domainForm);
        }

        public object DeserializeDomainFormAsync(XmlReader reader)
        {
            return XmlSerializerHelper.DeserializeObject<TDomain>(reader);
        }

        public Task SerializeDocumentFormAsync(Stream utf8Json, object form, CancellationToken cancellationToken)
        {
            return EAUJsonSerializer.SerializeAsync(utf8Json, form, typeof(TView), null, cancellationToken);
        }

        public async Task<object> DeserializeDocumentFormAsync(Stream jsonStream, CancellationToken cancellationToken)
        {
            return await EAUJsonSerializer.DeserializeAsync<TView>(jsonStream, null, cancellationToken);
        }

        public Task<DocumentFormData> TransformToDocumentFormAsync(object domainForm, CancellationToken cancellationToken)
        {
            var formData = new DocumentFormData();

            formData.AttachedDocuments = GetAttachedDocumentsInternal((TDomain)domainForm);

            formData.Form = Mapper.Map<TView>(domainForm);

            //Зачиства секциите(РИО обектите), които са празни.
            ObjectUtility.ClearEmptyFields(formData.Form);

            return Task.FromResult(formData);
        }

        public Task<object> TransformToDomainFormAsync(DocumentFormData documentFormData, CancellationToken cancellationToken)
        {
            var domainForm = Mapper.Map<TDomain>(documentFormData.Form);

            //Зачиства секциите(РИО обектите), които са празни.
            ObjectUtility.ClearEmptyFields(domainForm);

            PrepareDomainDocumentInternal(domainForm, documentFormData.AttachedDocuments);

            return Task.FromResult((object)domainForm);
        }

        public object CreateDocumentForm()
        {
            return Activator.CreateInstance(typeof(TView));
        }

        public abstract string SignatureXpath { get; }

        public abstract Dictionary<string, string> SignatureXPathNamespaces { get; }

        public async Task<List<object>> GetDocumentSignatures(XmlDocument request)
        {
            List<DigitalSignatureContainerVM> result = null;
            Stream docStream = null;

            try
            {
                ISigningService signingService = GetService<ISigningService>();

                docStream = XmlHelpers.GetStreamFromXmlDocument(request);

                SignatursVerificationResponse validationResult = await signingService.ValidateDocumentSignatures(docStream, "document.xml");

                if (validationResult != null
                    && validationResult.SignaturesCount.HasValue
                    && validationResult.SignaturesCount.Value > 0)
                {
                    result = new List<DigitalSignatureContainerVM>(validationResult.SignaturesCount.Value);

                    var signatersDataFromXml = GetSignaturesDataFromXml(request);

                    foreach (SignatuerData item in validationResult.Signatures)
                    {
                        byte[] certBytes = System.Text.Encoding.UTF8.GetBytes(item.SignerBase64EncodedCertificate);
                        var cert = new System.Security.Cryptography.X509Certificates.X509Certificate2(certBytes);

                        SigningCertificateData certData = new SigningCertificateData()
                        {
                            Issuer = cert.Issuer,
                            SerialNumber = cert.SerialNumber,
                            Subject = cert.Subject,
                            SubjectAlternativeName = cert.Extensions["Subject Alternative Name"] != null ? cert.Extensions["Subject Alternative Name"].Format(false) : string.Empty,
                            ValidFrom = DateTime.Parse(cert.GetEffectiveDateString()),
                            ValidTo = DateTime.Parse(cert.GetExpirationDateString())
                        };

                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        DateTime? signatureTime = item.SignatureTime == null || item.SignatureTime.Value == 0 ? (DateTime?)null : start.AddMilliseconds(item.SignatureTime.Value).ToLocalTime();

                        List<TimeStampInfo> timeStampInfos = null;

                        if (item.TimestampsDtos != null && item.TimestampsDtos.Count > 0)
                        {
                            timeStampInfos = new List<TimeStampInfo>(item.TimestampsDtos.Count);

                            foreach (TimestampToken token in item.TimestampsDtos)
                            {
                                foreach (string certPubKey in token.TimestampBase64EncodedCertificates)
                                {
                                    byte[] tokenCertBytes = System.Text.Encoding.UTF8.GetBytes(certPubKey);
                                    var tokenCert = new System.Security.Cryptography.X509Certificates.X509Certificate2(tokenCertBytes);

                                    timeStampInfos.Add(new TimeStampInfo()
                                    {
                                        SigningCertificateData = new SigningCertificateData()
                                        {
                                            Issuer = tokenCert.Issuer,
                                            SerialNumber = tokenCert.SerialNumber,
                                            Subject = tokenCert.Subject,
                                            SubjectAlternativeName = tokenCert.Extensions["Subject Alternative Name"] != null ? tokenCert.Extensions["Subject Alternative Name"].Format(false) : string.Empty,
                                            ValidFrom = DateTime.Parse(tokenCert.GetEffectiveDateString()),
                                            ValidTo = DateTime.Parse(tokenCert.GetExpirationDateString())
                                        },
                                        TimeStampTime = start.AddMilliseconds(token.TimestampGenerationTime.Value).ToLocalTime()
                                    });
                                }
                            }
                        }

                        string signatureUniqueID = signatersDataFromXml.First(sd =>
                            string.Compare(sd.PublicKey, item.SignerBase64EncodedCertificate, true) == 0).Id;

                        DigitalSignatureContainerVM tmp = new DigitalSignatureContainerVM()
                        {
                            Signature = new Signature()
                            {
                                IsValid = string.Compare(item.SignatureValid, "TRUE", true) == 0 ? true : false,
                                Error = string.Compare(item.SignatureValid, "TRUE", true) == 0 ? null : string.Join(",", item.SignatureErrorsList.ToArray()),
                                SignatureTime = signatureTime,
                                TimeStampInfos = timeStampInfos,
                                SigningCertificateData = certData,
                                SignatureUniqueID = item.SignatureId,
                            },
                            SignatureUniqueID = signatureUniqueID
                        };

                        result.Add(tmp);
                    }
                }

                return result != null ? result.Cast<object>().ToList() : null;
            }
            finally
            {
                if (docStream != null)
                {
                    docStream.Dispose();
                }
            }

        }

        #endregion

        #region IDocumentFormValidationService

        public Task<IErrorCollection> ValidateAsync(DocumentFormData form, CancellationToken cancellationToken)
        {
            return ValidateDocumentFormInternalAsync(form, cancellationToken);
        }

        public IErrorCollection ValidateByXSD(XmlDocument formXml, bool useWeakenedSchema)
        {
            IXmlSchemaValidator validator = null;
            var formServiceProvider = GetService<IDocumentFormServiceProvider>();


            if (useWeakenedSchema)
            {
                validator = formServiceProvider.GetRequiredService<IWeakenedXmlSchemaValidator>(DocumentTypeUri);
            }
            else
            {
                validator = formServiceProvider.GetRequiredService<IXmlSchemaValidator>(DocumentTypeUri);
            }

            var shemaValResult = validator.ValidateXml(formXml, formXml.DocumentElement.NamespaceURI).ToList();

            if (shemaValResult.Count > 0)
            {
                var logger = GetService<ILogger<DocumentFormServiceBase<TDomain, TView>>>();
                string errMessage = "";

                foreach (var err in shemaValResult)
                {
                    errMessage = errMessage + err.Message + Environment.NewLine;
                }

                logger.LogError("Error durring document xsd validation:" + errMessage);

                return new ErrorCollection()
                {
                    new TextError("DOC_GL_INCORRECT_DOCUMENT_XML_CONTENT_E","DOC_GL_INCORRECT_DOCUMENT_XML_CONTENT_E")
                };
            }

            return new ErrorCollection();
        }

        public IErrorCollection ValidateDomainForm(object domainForm)
        {
            var validatorFactory = GetService<IValidatorFactory>();

            var validator = validatorFactory.GetValidator<TDomain>();

            var result = new ErrorCollection();

            ////TODO da se sloji proverka ako nqma validator da wrysta greshka - 
            if (validator != null)
            {
                var validationContext = new FluentValidation.ValidationContext<TDomain>((TDomain)domainForm
                    , new PropertyChain()
                    , ValidatorOptions.Global.ValidatorSelectors.DefaultValidatorSelectorFactory());

                validationContext.SetServiceProvider(ServiceProvider);

                var fluentResult = validator.Validate(validationContext);

                if (!fluentResult.IsValid)
                {
                    foreach (var item in fluentResult.Errors)
                    {
                        //TextError се инициализира с ErrorMessage само, защото в BaseApiController за връщане на BadRequest
                        //се прави опит за локализация на кода на всеки обект от тип ApiError, при което се замазва преведеното
                        //вече съобщение за грешка от FluentValidator - а.
                        result.Add(new TextError(item.ErrorMessage, item.ErrorMessage));
                    }
                }
            }

            return result;
        }

        #endregion

        #region IDocumentFormPrintService

        public async Task GetPrintPreviewHtmlAsync(TextWriter writer, XmlDocument domainForm, string appicationPath, CancellationToken cancellationToken)
        {
            var signatures = await GetDocumentSignatures(domainForm);

            DocumentSignatures ds = new DocumentSignatures() { Signatures = new List<Signature>() };
            if (signatures != null)
            {
                foreach (var sgn in signatures)
                {
                    ds.Signatures.Add(((DigitalSignatureContainerVM)sgn).Signature);
                }
            }
            string signature = XmlSerializerHelper.SerializeObjectUtf8(ds);

            XmlDocument signDocument = new XmlDocument();

            if (!string.IsNullOrEmpty(signature))
                signDocument.LoadXml(signature);

            XsltArgumentList arguments = new XsltArgumentList();

            arguments.AddParam("SignatureXML", string.Empty, signDocument.CreateNavigator().Select("/."));
            arguments.AddParam("ApplicationPath", string.Empty, appicationPath);
            arguments.AddExtensionObject("urn:XSLExtension", new XSLExtensions());

            XslCompiledTransform transform = new XslCompiledTransform();

            transform.Load(PrintPreviewData.Xslt, XsltSettings.TrustedXslt, PrintPreviewData.Resolver);

            transform.Transform(domainForm, arguments, writer);
        }

        #endregion

        #region Virtual

        protected virtual Task<IErrorCollection> ValidateDocumentFormInternalAsync(DocumentFormData form, CancellationToken cancellationToken)
        {
            return Task.FromResult((IErrorCollection)new ErrorCollection());
        }

        protected virtual void PrepareDomainDocumentInternal(TDomain formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            
        }

        protected virtual List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> GetAttachedDocumentsInternal(TDomain formDoamin)
        {
            return null;
        }

        protected virtual Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            if (request.Form is SigningDocumentFormVMBase<OfficialVM> formO)
            {
                if (request.Signatures != null && request.Signatures.Count > 0)
                {
                    if (formO.DigitalSignatures != null && formO.DigitalSignatures.Count > 0)
                    {
                        formO.DigitalSignatures[0].Signature = ((DigitalSignatureContainerVM)request.Signatures[0]).Signature;
                    }
                    else
                    {
                        formO.DigitalSignatures = request.Signatures.Select(s => new OfficialVM()
                        {
                            Signature = ((DigitalSignatureContainerVM)s).Signature,
                            SignatureUniqueID = ((DigitalSignatureContainerVM)s).SignatureUniqueID
                        }).ToList();
                    }
                }
            }
            else if (request.Form is SigningDocumentFormVMBase<DigitalSignatureContainerVM> formDS)
            {
                if (request.Signatures != null && request.Signatures.Count > 0)
                {
                    formDS.DigitalSignatures = request.Signatures.Select(s => (DigitalSignatureContainerVM)s).ToList();
                }
            }

            return Task.FromResult(new OperationResult(OperationResultTypes.SuccessfullyCompleted));
        }

        #endregion

        #region Helper

        private List<SignatureData> GetSignaturesDataFromXml(XmlDocument xmlDocument)
        {
            XmlNode root = xmlDocument.DocumentElement;
            var nm = new XmlNamespaceManager(xmlDocument.NameTable);

            foreach (var item in SignatureXPathNamespaces)
            {
                nm.AddNamespace(item.Key, item.Value);
            }

            nm.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            nm.AddNamespace("xades", "http://uri.etsi.org/01903/v1.3.2#");

            string Xpath = SignatureXpath.StartsWith("//") ? SignatureXpath : string.Format("//{0}", SignatureXpath);
            XmlNode signaturePlaceHolderNode = root.SelectSingleNode(Xpath, nm);

            if (signaturePlaceHolderNode != null)
            {
                List<SignatureData> signatures = new List<SignatureData>();

                foreach (XmlNode signature in signaturePlaceHolderNode.ChildNodes)
                {
                    if (signature.LocalName == "Signature")
                    {
                        SignatureData tmp = new SignatureData();
                        tmp.Id = signature.Attributes["Id"].Value;

                        string publicKeyXPath = "//ds:Signature/ds:KeyInfo/ds:X509Data/ds:X509Certificate";
                        XmlNode publicKeyNode = signature.SelectSingleNode(publicKeyXPath, nm);
                        tmp.PublicKey = publicKeyNode.InnerText.Replace(Environment.NewLine, "").Replace("\t", ""); // replace-a се прави, защото в някои xml-и публичния ключ на подписа идва pem форматиран!

                        //Това го махнах по искане на Мотовски. 
                        //Когато имаме повече от един подпис и имаме подписи направени с един и същ сертификат може да не се вземе правилното signatureUniqueID.
                        //Защото тогава тр да се гледа и времето на подписване.
                        //string signTimeXPath = "//ds:Signature/ds:Object/xades:QualifyingProperties/xades:SignedProperties/xades:SignedSignatureProperties/xades:SigningTime";
                        //XmlNode signTimeNode = signature.SelectSingleNode(signTimeXPath, nm);
                        //tmp.SignDate = DateTime.Parse(signTimeNode.InnerText, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal);

                        signatures.Add(tmp);
                    }
                }

                return signatures;
            }

            return null;
        }

        public virtual Task<DocumentFormData> BuildWithdrawServiceFormAsync(object domainForm, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    internal class SignatureData
    {
        public string Id { get; set; }

        public string PublicKey { get; set; }

        public DateTime SignDate { get; set; }
    }
}
