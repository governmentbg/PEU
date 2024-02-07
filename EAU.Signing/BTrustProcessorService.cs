using CNSys;
using CNSys.Xml;
using EAU.Signing.BSecureDSSL;
using EAU.Signing.BtrustRemoteClient;
using EAU.Signing.Configuration;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EAU.Signing
{
    public interface IBTrustProcessorService
    {
        Task<OperationResult<BissSignRequestExtended>> CreateBissSignRequestAsync(Guid processID, string Base64SigningCert, CancellationToken cancellationToken);

        Task<OperationResult<BissSignRequestExtended>> CreateTestBissSignRequest(string UserCertBase64);

        Task<OperationResult> CompleteBissSignProcessAsync(Guid processID, string Base64SigningCert, string Base64DocSignature, long hashTime, long signerID, CancellationToken cancellationToken);

        Task<OperationResult> CompleteTestBissSignProcess(string Base64SigningCert, string Base64DocSignature, long hashTime);

        Task<OperationResult> CreateRemoteSignRequestAsync(Guid processID, long SignerID, BtrustUserInputRequest btrustUserInput, CancellationToken cancellationToken);

        Task<OperationResult<BtrustPullingResult>> TryCompleteRemoteSigning(Guid processID, long SignerID, CancellationToken cancellationToken);
    }

    internal class BTrustProcessorService : IBTrustProcessorService
    {
        #region Members

        //private readonly static string _testSignXml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><DeclarationUndurArticle17 xmlns=\"http://ereg.egov.bg/segment/R-3051\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><ElectronicAdministrativeServiceHeader><SUNAUServiceURI xmlns=\"http://ereg.egov.bg/segment/0009-000152\">66667</SUNAUServiceURI><DocumentTypeURI xmlns=\"http://ereg.egov.bg/segment/0009-000152\"><RegisterIndex xmlns=\"http://ereg.egov.bg/segment/0009-000022\">10</RegisterIndex><BatchNumber xmlns=\"http://ereg.egov.bg/segment/0009-000022\">3051</BatchNumber></DocumentTypeURI><DocumentTypeName xmlns=\"http://ereg.egov.bg/segment/0009-000152\">Декларация по чл.17, ал.1 от Правилника за издаване на българските лични документи</DocumentTypeName><ElectronicServiceProviderBasicData xmlns=\"http://ereg.egov.bg/segment/0009-000152\"><EntityBasicData xmlns=\"http://ereg.egov.bg/segment/0009-000002\"><Name xmlns=\"http://ereg.egov.bg/segment/0009-000013\">Министерство на вътрешните работи</Name><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000013\">126608480</Identifier></EntityBasicData><ElectronicServiceProviderType xmlns=\"http://ereg.egov.bg/segment/0009-000002\">0006-000031</ElectronicServiceProviderType></ElectronicServiceProviderBasicData><ElectronicServiceApplicant xmlns=\"http://ereg.egov.bg/segment/0009-000152\"><RecipientGroup xmlns=\"http://ereg.egov.bg/segment/0009-000016\"><Author><AuthorQualityType xmlns=\"http://ereg.egov.bg/segment/0009-000012\">R-1001</AuthorQualityType><Person xmlns=\"http://ereg.egov.bg/segment/0009-000012\"><Names xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><First xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ЧАВДАР</First><Middle xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ИВАНОВ</Middle><Last xmlns=\"http://ereg.egov.bg/segment/0009-000005\">НАЧЕВ</Last></Names><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><EGN xmlns=\"http://ereg.egov.bg/segment/0009-000006\">5012117564</EGN></Identifier><IdentityDocument xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><IdentityNumber xmlns=\"http://ereg.egov.bg/segment/0009-000099\">645723723</IdentityNumber><IdentitityIssueDate xmlns=\"http://ereg.egov.bg/segment/0009-000099\">2015-03-23</IdentitityIssueDate><IdentityIssuer xmlns=\"http://ereg.egov.bg/segment/0009-000099\">ОДМВР ШУМЕН</IdentityIssuer><IdentityDocumentType xmlns=\"http://ereg.egov.bg/segment/0009-000099\">0006-000087</IdentityDocumentType></IdentityDocument></Person></Author><Recipient><Person xmlns=\"http://ereg.egov.bg/segment/0009-000015\"><Names xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><First xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ЧАВДАР</First><Middle xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ИВАНОВ</Middle><Last xmlns=\"http://ereg.egov.bg/segment/0009-000005\">НАЧЕВ</Last></Names><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><EGN xmlns=\"http://ereg.egov.bg/segment/0009-000006\">5012117564</EGN></Identifier><IdentityDocument xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><IdentityNumber xmlns=\"http://ereg.egov.bg/segment/0009-000099\">645723723</IdentityNumber><IdentitityIssueDate xmlns=\"http://ereg.egov.bg/segment/0009-000099\">2015-03-23</IdentitityIssueDate><IdentityIssuer xmlns=\"http://ereg.egov.bg/segment/0009-000099\">ОДМВР ШУМЕН</IdentityIssuer><IdentityDocumentType xmlns=\"http://ereg.egov.bg/segment/0009-000099\">0006-000087</IdentityDocumentType></IdentityDocument></Person></Recipient></RecipientGroup><EmailAddress xmlns=\"http://ereg.egov.bg/segment/0009-000016\">g.georgiev@cnsys.bg</EmailAddress></ElectronicServiceApplicant><ApplicationType xmlns=\"http://ereg.egov.bg/segment/0009-000152\">0006-000121</ApplicationType><SUNAUServiceName xmlns=\"http://ereg.egov.bg/segment/0009-000152\">ЕАУ-54 Подаване на декларация по чл.17, ал.1 от Правилника за издаване на българските лични документи при изгубване/ кражба/ повреждане/ унищожаване на лична карта</SUNAUServiceName><SendApplicationWithReceiptAcknowledgedMessage xmlns=\"http://ereg.egov.bg/segment/0009-000152\">false</SendApplicationWithReceiptAcknowledgedMessage></ElectronicAdministrativeServiceHeader><ServiceTermType>0006-000083</ServiceTermType><ServiceApplicantReceiptData><ServiceResultReceiptMethod xmlns=\"http://ereg.egov.bg/segment/0009-000141\">R-6001</ServiceResultReceiptMethod><UnitInAdministration xmlns=\"http://ereg.egov.bg/segment/0009-000141\"><EntityBasicData><Name xmlns=\"http://ereg.egov.bg/segment/0009-000013\">Министерство на вътрешните работи</Name><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000013\">126608480</Identifier></EntityBasicData><AdministrativeDepartmentName>СДВР РУ 01</AdministrativeDepartmentName><AdministrativeDepartmentCode>225</AdministrativeDepartmentCode></UnitInAdministration></ServiceApplicantReceiptData><DeclarationUndurArticle17Data><DocType xmlns=\"http://ereg.egov.bg/segment/R-3052\">0</DocType><PermanentAddress xmlns=\"http://ereg.egov.bg/segment/R-3052\"><DistrictGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">22</DistrictGRAOCode><DistrictGRAOName xmlns=\"http://ereg.egov.bg/segment/0009-000094\">СОФИЯ</DistrictGRAOName><MunicipalityGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">46</MunicipalityGRAOCode><MunicipalityGRAOName xmlns=\"http://ereg.egov.bg/segment/0009-000094\">СТОЛИЧНА</MunicipalityGRAOName><SettlementGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">68134</SettlementGRAOCode><SettlementGRAOName xmlns=\"http://ereg.egov.bg/segment/0009-000094\">ГР.СОФИЯ</SettlementGRAOName><StreetGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">78063</StreetGRAOCode><StreetText xmlns=\"http://ereg.egov.bg/segment/0009-000094\">Цар Асен</StreetText><BuildingNumber xmlns=\"http://ereg.egov.bg/segment/0009-000094\">1</BuildingNumber><Entrance xmlns=\"http://ereg.egov.bg/segment/0009-000094\">А</Entrance><Floor xmlns=\"http://ereg.egov.bg/segment/0009-000094\">1</Floor><Apartment xmlns=\"http://ereg.egov.bg/segment/0009-000094\">1</Apartment></PermanentAddress><ReasonData xmlns=\"http://ereg.egov.bg/segment/R-3052\"><Reason xmlns=\"http://ereg.egov.bg/segment/R-3053\">0</Reason><FactsAndCircumstances xmlns=\"http://ereg.egov.bg/segment/R-3053\">Test</FactsAndCircumstances></ReasonData></DeclarationUndurArticle17Data><Declarations><Declaration><IsDeclarationFilled xmlns=\"http://ereg.egov.bg/segment//R-3136\">true</IsDeclarationFilled><DeclarationName xmlns=\"http://ereg.egov.bg/segment//R-3136\">&lt;p&gt;Запознат съм с прилаганата в МВР политика за поверителност, съгласно изискванията на Общия регламент относно защитата на данните (Регламент (ЕС) 2016/679 - GDPR).&lt;/p&gt;\r\n</DeclarationName><DeclarationCode xmlns=\"http://ereg.egov.bg/segment//R-3136\">Policy_GDPR</DeclarationCode></Declaration></Declarations><ElectronicAdministrativeServiceFooter><ApplicationSigningTime xmlns=\"http://ereg.egov.bg/segment/0009-000153\">2023-08-28T15:42:00.1538248+03:00</ApplicationSigningTime><XMLDigitalSignature xmlns=\"http://ereg.egov.bg/segment/0009-000153\">\r\n\t\t</XMLDigitalSignature></ElectronicAdministrativeServiceFooter></DeclarationUndurArticle17>\r\n";
        private readonly static string _testSignXml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"no\"?><ApplicationByFormAnnex10 xmlns=\"http://ereg.egov.bg/segment/R-3113\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><ElectronicAdministrativeServiceHeader><SUNAUServiceURI xmlns=\"http://ereg.egov.bg/segment/0009-000152\">1068</SUNAUServiceURI><DocumentTypeURI xmlns=\"http://ereg.egov.bg/segment/0009-000152\"><RegisterIndex xmlns=\"http://ereg.egov.bg/segment/0009-000022\">10</RegisterIndex><BatchNumber xmlns=\"http://ereg.egov.bg/segment/0009-000022\">3113</BatchNumber></DocumentTypeURI><DocumentTypeName xmlns=\"http://ereg.egov.bg/segment/0009-000152\">Заявление по образец Приложение № 10</DocumentTypeName><ElectronicServiceProviderBasicData xmlns=\"http://ereg.egov.bg/segment/0009-000152\"><EntityBasicData xmlns=\"http://ereg.egov.bg/segment/0009-000002\"><Name xmlns=\"http://ereg.egov.bg/segment/0009-000013\">МИНИСТЕРСТВО НА ВЪТРЕШНИТЕ РАБОТИ</Name><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000013\">000695235</Identifier></EntityBasicData><ElectronicServiceProviderType xmlns=\"http://ereg.egov.bg/segment/0009-000002\">0006-000031</ElectronicServiceProviderType></ElectronicServiceProviderBasicData><ElectronicServiceApplicant xmlns=\"http://ereg.egov.bg/segment/0009-000152\"><RecipientGroup xmlns=\"http://ereg.egov.bg/segment/0009-000016\"><Author><AuthorQualityType xmlns=\"http://ereg.egov.bg/segment/0009-000012\">R-1001</AuthorQualityType><Person xmlns=\"http://ereg.egov.bg/segment/0009-000012\"><Names xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><First xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ПЕТЪР</First><Last xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ПЕТРОВ</Last></Names><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><EGN xmlns=\"http://ereg.egov.bg/segment/0009-000006\">1010101010</EGN></Identifier><IdentityDocument xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><IdentityNumber xmlns=\"http://ereg.egov.bg/segment/0009-000099\">800003229</IdentityNumber><IdentitityIssueDate xmlns=\"http://ereg.egov.bg/segment/0009-000099\">2016-01-01</IdentitityIssueDate><IdentityIssuer xmlns=\"http://ereg.egov.bg/segment/0009-000099\">МВР СОФИЯ</IdentityIssuer><IdentityDocumentType xmlns=\"http://ereg.egov.bg/segment/0009-000099\">0006-000087</IdentityDocumentType></IdentityDocument></Person></Author><Recipient><Person xmlns=\"http://ereg.egov.bg/segment/0009-000015\"><Names xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><First xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ПЕТЪР</First><Last xmlns=\"http://ereg.egov.bg/segment/0009-000005\">ПЕТРОВ</Last></Names><Identifier xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><EGN xmlns=\"http://ereg.egov.bg/segment/0009-000006\">1010101010</EGN></Identifier><IdentityDocument xmlns=\"http://ereg.egov.bg/segment/0009-000008\"><IdentityNumber xmlns=\"http://ereg.egov.bg/segment/0009-000099\">800003229</IdentityNumber><IdentitityIssueDate xmlns=\"http://ereg.egov.bg/segment/0009-000099\">2016-01-01</IdentitityIssueDate><IdentityIssuer xmlns=\"http://ereg.egov.bg/segment/0009-000099\">МВР СОФИЯ</IdentityIssuer><IdentityDocumentType xmlns=\"http://ereg.egov.bg/segment/0009-000099\">0006-000087</IdentityDocumentType></IdentityDocument></Person></Recipient></RecipientGroup><EmailAddress xmlns=\"http://ereg.egov.bg/segment/0009-000016\">MARIYA.HRISTOVA@CNSYS.BG</EmailAddress></ElectronicServiceApplicant><ApplicationType xmlns=\"http://ereg.egov.bg/segment/0009-000152\">0006-000121</ApplicationType><SUNAUServiceName xmlns=\"http://ereg.egov.bg/segment/0009-000152\">Издаване на разрешения за съхранение на огнестрелни оръжия и боеприпаси за тях от физически лица</SUNAUServiceName><SendApplicationWithReceiptAcknowledgedMessage xmlns=\"http://ereg.egov.bg/segment/0009-000152\">false</SendApplicationWithReceiptAcknowledgedMessage></ElectronicAdministrativeServiceHeader><ServiceTermType>0006-000083</ServiceTermType><ServiceApplicantReceiptData><ServiceResultReceiptMethod xmlns=\"http://ereg.egov.bg/segment/0009-000141\">0006-000077</ServiceResultReceiptMethod></ServiceApplicantReceiptData><ApplicationByFormAnnex10Data><PersonalInformation xmlns=\"http://ereg.egov.bg/segment/R-3114\"><PersonAddress xmlns=\"http://ereg.egov.bg/segment/R-3015\"><DistrictGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">10</DistrictGRAOCode><DistrictGRAOName xmlns=\"http://ereg.egov.bg/segment/0009-000094\">КЮСТЕНДИЛ</DistrictGRAOName><MunicipalityGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">29</MunicipalityGRAOCode><MunicipalityGRAOName xmlns=\"http://ereg.egov.bg/segment/0009-000094\">КЮСТЕНДИЛ</MunicipalityGRAOName><SettlementGRAOCode xmlns=\"http://ereg.egov.bg/segment/0009-000094\">21782</SettlementGRAOCode><SettlementGRAOName xmlns=\"http://ereg.egov.bg/segment/0009-000094\">С.ДОЖДЕВИЦА</SettlementGRAOName><StreetText xmlns=\"http://ereg.egov.bg/segment/0009-000094\">УЛ.ДОСТОЕВСКИ Ф.М.</StreetText><BuildingNumber xmlns=\"http://ereg.egov.bg/segment/0009-000094\">6</BuildingNumber></PersonAddress></PersonalInformation><IssuingDocument xmlns=\"http://ereg.egov.bg/segment/R-3114\">РАЗРЕШЕНИЕ ЗА СЪХРАНЕНИЕ НА ОГНЕСТРЕЛНИ ОРЪЖИЯ И БОЕПРИПАСИ ЗА ТЯХ ОТ ФИЗИЧЕСКИ ЛИЦА</IssuingDocument></ApplicationByFormAnnex10Data><IssuingPoliceDepartment><PoliceDepartmentCode xmlns=\"http://ereg.egov.bg/segment/R-3037\">225</PoliceDepartmentCode><PoliceDepartmentName xmlns=\"http://ereg.egov.bg/segment/R-3037\">СДВР РУ 01</PoliceDepartmentName></IssuingPoliceDepartment><Declarations><Declaration><IsDeclarationFilled xmlns=\"http://ereg.egov.bg/segment//R-3136\">false</IsDeclarationFilled><DeclarationName xmlns=\"http://ereg.egov.bg/segment//R-3136\">Декларирам, че придобитите огнестрелни оръжия ще се съхраняват по изискванията на ЗОБВВПИ.</DeclarationName><DeclarationCode xmlns=\"http://ereg.egov.bg/segment//R-3136\">16</DeclarationCode></Declaration></Declarations><ElectronicAdministrativeServiceFooter><ApplicationSigningTime xmlns=\"http://ereg.egov.bg/segment/0009-000153\">2020-05-21T10:14:25.6742695+03:00</ApplicationSigningTime><XMLDigitalSignature xmlns=\"http://ereg.egov.bg/segment/0009-000153\"></XMLDigitalSignature></ElectronicAdministrativeServiceFooter></ApplicationByFormAnnex10>";

        private readonly ISigningProcessesService _signingProcessesService;
        private readonly IBtrustRemoteClientFactory _btrustRemoteClientFactory;
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<SignModuleGlobalOptions> _signModuleOptions;
        private readonly IDocumentSigningtUtilityService _documentSigningUtilityService;

        #endregion

        #region Constructor

        public BTrustProcessorService(ISigningProcessesService signingProcessesService
                                      , IBtrustRemoteClientFactory btrustRemoteClientFactory
                                      , ILogger<BTrustProcessorService> logger
                                      , IOptionsMonitor<SignModuleGlobalOptions> signModuleOptions
                                      , IDocumentSigningtUtilityService documentSigningUtilityService)
        {
            _signingProcessesService       = signingProcessesService;
            _btrustRemoteClientFactory     = btrustRemoteClientFactory;
            _logger                        = logger;
            _signModuleOptions             = signModuleOptions;
            _documentSigningUtilityService = documentSigningUtilityService;
        }

        #endregion

        #region IBTrustProcessorService

        #region BISS

        public async Task<OperationResult<BissSignRequestExtended>> CreateBissSignRequestAsync(Guid processID, string Base64SigningCert, CancellationToken cancellationToken)
        {
            SigningProcess process = null;
            try
            {
                process = (await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadContent = true,
                    LoadSigners = true
                }, cancellationToken)).SingleOrDefault();

                if (process == null || process.Content == null)
                    return new OperationResult<BissSignRequestExtended>("GL_SIGN_NO_DATA_E", "GL_SIGN_NO_DATA_E");

                //Това е за да хванем ако има инициирано отдалечено подписване. 
                if (process.Signers.Any(s => s.Status == SignerSigningStatuses.StartSigning))
                    return new OperationResult<BissSignRequestExtended>("GL_SIGN_ONGOING_SIGNING_E", "GL_SIGN_ONGOING_SIGNING_E");

                return await CreateBissSignRequestInternal(process, Base64SigningCert);
            }
            finally
            {
                if(process != null && process.Content != null)
                {
                    process.Content.Dispose();
                }
            }
        }

        public async Task<OperationResult> CompleteBissSignProcessAsync(Guid processID, string Base64SigningCert, string Base64DocSignature, long hashTime, long signerID, CancellationToken cancellationToken)
        {
            SigningProcess process = null;

            try
            {
                process =(await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadContent = true,
                    LoadSigners = true
                }, cancellationToken)).SingleOrDefault();

                if (process == null || process.Content == null)
                    return new OperationResult("GL_NO_DATA_FOUND_L", "GL_NO_DATA_FOUND_L");

                Signer currentSigner = process.Signers.SingleOrDefault(s => s.SignerID == signerID);

                if (currentSigner == null || currentSigner.Status == SignerSigningStatuses.Signed)
                    return new OperationResult("GL_SIGN_ONGOING_SIGNING_E", "GL_SIGN_ONGOING_SIGNING_E");

                using (Stream assembledDoc = await _documentSigningUtilityService.AssembleDocumentWithSignatureAsync(process.ContentType
                                                                                                , process.FileName
                                                                                                , process.Content
                                                                                                , process.DigestMethod.Value.ToString()
                                                                                                , process.Format.Value
                                                                                                , process.Type.Value.ToString()
                                                                                                , process.Level.Value
                                                                                                , Base64SigningCert
                                                                                                , Base64DocSignature
                                                                                                , hashTime
                                                                                                , process.AdditionalData.SignatureXpath
                                                                                                , process.AdditionalData.SignatureXPathNamespaces
                                                                                                , process.Format == SigningFormats.PAdES))
                {
                    //Това се прави за оптимизация.
                    if (process.Content != null)
                    {
                        process.Content.Dispose();
                        process.Content = null;
                    }

                    return await _signingProcessesService.SignerSignedLocalAsync(processID, signerID, assembledDoc, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                //Грешка при подписване. Моля, свържете се с администратор на Портала на Агенция по вписванията.
                //След отстраняване на проблема, опитайте да подпишете отново.
                return new OperationResult("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            finally
            {
                if (process != null && process.Content != null)
                {
                    process.Content.Dispose();
                    process.Content = null;
                }
            }
        }

        public async Task<OperationResult<BissSignRequestExtended>> CreateTestBissSignRequest(string UserCertBase64)
        {
            using(var ms = CNSys.Xml.XmlHelpers.GetStreamFromXmlDocument(_testSignXml))
            {
                SigningProcess process = new SigningProcess()
                {
                    FileName = "Document.xml",
                    ContentType = "text/xml",
                    Content = ms,
                    Format = SigningFormats.XAdES,
                    Level = SigningLevels.BASELINE_LT,
                    DigestMethod = DigestMethods.SHA256,
                    Type = SigningPackingTypes.ENVELOPED,
                    AdditionalData = new SignProcessAdditionalData() 
                    {
                        //SignatureXpath = "d:DeclarationUndurArticle17/d:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature",
                        //SignatureXPathNamespaces = new Dictionary<string, string>()
                        //{
                        //    {"d", "http://ereg.egov.bg/segment/R-3051" },
                        //    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153" }
                        //}
                        SignatureXpath = "aid:ApplicationByFormAnnex10/aid:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature",
                        SignatureXPathNamespaces = new Dictionary<string, string>()
                        {
                            {"aid", "http://ereg.egov.bg/segment/R-3113" },
                            {"xmldsig", "http://ereg.egov.bg/segment/0009-000153" }
                        }
                    }
                };

                return await CreateBissSignRequestInternal(process, UserCertBase64);
            }
        }

        public async Task<OperationResult> CompleteTestBissSignProcess(string Base64SigningCert, string Base64DocSignature, long hashTime)
        {
            SigningProcess process = null;
            try
            {
                process = new SigningProcess()
                {
                    FileName = "Document.xml",
                    ContentType = "text/xml",
                    Content = CNSys.Xml.XmlHelpers.GetStreamFromXmlDocument(_testSignXml),
                    Format = SigningFormats.XAdES,
                    Level = SigningLevels.BASELINE_LT,
                    DigestMethod = DigestMethods.SHA256,
                    Type = SigningPackingTypes.ENVELOPED,
                    AdditionalData = new SignProcessAdditionalData()
                    {
                        //SignatureXpath = "d:DeclarationUndurArticle17/d:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature",
                        //SignatureXPathNamespaces = new Dictionary<string, string>()
                        //{
                        //    {"d", "http://ereg.egov.bg/segment/R-3051" },
                        //    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153" }
                        //}
                        SignatureXpath = "aid:ApplicationByFormAnnex10/aid:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature",
                        SignatureXPathNamespaces = new Dictionary<string, string>()
                        {
                            {"aid", "http://ereg.egov.bg/segment/R-3113" },
                            {"xmldsig", "http://ereg.egov.bg/segment/0009-000153" }
                        }
                    }
                };

                using (Stream assembledDoc = await _documentSigningUtilityService.AssembleDocumentWithSignatureAsync(process.ContentType
                                                                                                , process.FileName
                                                                                                , process.Content
                                                                                                , process.DigestMethod.Value.ToString()
                                                                                                , process.Format.Value
                                                                                                , process.Type.Value.ToString()
                                                                                                , process.Level.Value
                                                                                                , Base64SigningCert
                                                                                                , Base64DocSignature
                                                                                                , hashTime
                                                                                                , process.AdditionalData.SignatureXpath
                                                                                                , process.AdditionalData.SignatureXPathNamespaces
                                                                                                , process.Format == SigningFormats.PAdES))
                {
                    if(process.Content != null)
                    {
                        process.Content.Dispose();
                        process.Content = null;
                    }

                    #region Валидация на подписите.

                    var validationResult = await _documentSigningUtilityService.SignaturesVerificationAsync(assembledDoc, process.FileName);
                    bool isValid = string.Compare(validationResult.DocumentStatusValid, "TRUE", true) == 0 && validationResult.Signatures.Count() > 0;

                    if (!isValid)
                    {
                        _logger.LogWarning("Неуспешна валидация на подпис. Съобщение за грешка: {message}. Брой валидни подписи: {validSignaturesCount}. Дата на валидация: {validationDateTime}. Подписи в документа: {newLine} {signatures}"
                        , validationResult.Message
                        , validationResult.ValidSignaturesCount
                        , validationResult.ValidationDateTime
                        , Environment.NewLine
                        , validationResult.Signatures != null && validationResult.Signatures.Any() ? string.Join(Environment.NewLine, validationResult.Signatures.Select(el => el.ToJson())) : "");

                        //В документа е положен невалиден подпис.
                        return new OperationResult("GL_SIGN_INAVLD_SIGNATURE_E", "GL_SIGN_INAVLD_SIGNATURE_E");
                    }

                    #region Test

                    //using (var dstream = new MemoryStream())
                    //{
                    //    assembledDoc.CopyTo(dstream);

                    //    dstream.Position = 0;

                    //    XmlDocument d = XmlHelpers.CreateXmlDocument(dstream);

                    //    dstream.Position = 0;

                    //    var validationResult = await _documentSigningUtilityService.SignaturesVerificationAsync(dstream, process.FileName);
                    //    bool isValid = string.Compare(validationResult.DocumentStatusValid, "TRUE", true) == 0 && validationResult.Signatures.Count() > 0;

                    //    if (!isValid)
                    //    {
                    //        _logger.LogWarning("Неуспешна валидация на подпис. Съобщение за грешка: {message}. Брой валидни подписи: {validSignaturesCount}. Дата на валидация: {validationDateTime}. Подписи в документа: {newLine} {signatures}"
                    //        , validationResult.Message
                    //        , validationResult.ValidSignaturesCount
                    //        , validationResult.ValidationDateTime
                    //        , Environment.NewLine
                    //        , validationResult.Signatures != null && validationResult.Signatures.Any() ? string.Join(Environment.NewLine, validationResult.Signatures.Select(el => el.ToJson())) : "");

                    //        //В документа е положен невалиден подпис.
                    //        return new OperationResult("GL_SIGN_INAVLD_SIGNATURE_E", "GL_SIGN_INAVLD_SIGNATURE_E");
                    //    }
                    //}

                    #endregion

                    #endregion
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                return new OperationResult("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            finally
            {
                if (process != null && process.Content != null)
                {
                    process.Content.Dispose();
                    process.Content = null;
                }
            }
        }

        #endregion

        #region Remote

        public async Task<OperationResult> CreateRemoteSignRequestAsync(Guid processID, long SignerID, BtrustUserInputRequest btrustUserInput, CancellationToken cancellationToken)
        {
            SigningProcess process = null;

            try
            {
                process = (await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadContent = true,
                    LoadSigners = true
                }, cancellationToken)).SingleOrDefault();

                IBtrustRemoteClient btrustRemoteClient = _btrustRemoteClientFactory.GetBtrustRemoteClient();

                //1. Взимаме сертификата на потребителя
                string base64ClientCert = null;
                string rpToClientAuthorization = null;

                if (btrustUserInput.InputType == BtrustUserInputTypes.PROFILE)
                {
                    //Проверка на потребителя.
                    var authInfo = new AuthInfo() { ProfileId = btrustUserInput.Input, Otp = btrustUserInput.Otp };
                    var authorizationResponse = await btrustRemoteClient.V2AuthAsync("en", authInfo, _signModuleOptions.CurrentValue.SIGN_BTRUST_REMOTE_RELYINGPARTY_ID, cancellationToken);

                    rpToClientAuthorization = string.Format("clientToken:{0}", authorizationResponse.Data.ClientToken);

                    var clientCertResponse = await btrustRemoteClient.V2CertAsync("en", btrustUserInput.Input, _signModuleOptions.CurrentValue.SIGN_BTRUST_REMOTE_RELYINGPARTY_ID, cancellationToken);
                    base64ClientCert = clientCertResponse.Data.EncodedCert;
                }
                else
                {
                    var clientCertRes = await CheckAndGetUserCertAsync(btrustRemoteClient, btrustUserInput, cancellationToken);

                    if (!clientCertRes.IsSuccessfullyCompleted)
                        return clientCertRes;

                    base64ClientCert = clientCertRes.Result.Data.EncodedCert;

                    switch (btrustUserInput.InputType)
                    {
                        case BtrustUserInputTypes.EGN:
                        case BtrustUserInputTypes.LNCH:
                            rpToClientAuthorization = string.Format("personalId:{0}", btrustUserInput.Input);
                            break;
                        case BtrustUserInputTypes.EMAIL:
                        case BtrustUserInputTypes.PHONE:
                            rpToClientAuthorization = string.Format("certId:{0}", clientCertRes.Result.Data.CertReqId);
                            break;
                        default:
                            throw new NotSupportedException("Not supported BtrustUserInputTypes in this case");
                    }
                }

                //2. Генерира хеш на документа за подписване.
                DigestResponseDto bSecureHashResponse = null;
                using (process.Content)
                {
                    bSecureHashResponse = await _documentSigningUtilityService.CreateDocumentHashAsync(
                                                            process.ContentType
                                                            , process.FileName
                                                            , process.Content
                                                            , process.DigestMethod.Value.ToString()
                                                            , process.Format.Value
                                                            , process.Type.Value.ToString()
                                                            , process.Level.Value
                                                            , base64ClientCert
                                                            , process.AdditionalData.SignatureXpath
                                                            , process.AdditionalData.SignatureXPathNamespaces
                                                            , process.Format == SigningFormats.PAdES);
                }

                //3. Създава заявка за подписване
                string fileName = process.FileName;
                if (process.FileName.Length > 120 && Regex.IsMatch(process.FileName, @"\p{IsCyrillic}"))
                {
                    //Това се прави защото в базата на Btrust колоната е VARCHAR(250), затова е необходимо понеже имената са 
                    //на кирилица да намалим на половин дължината и така да се вместят в тяхната колона.
                    fileName = string.Format("{0}-{1}", process.FileName.Substring(0, 59), process.FileName.Substring(process.FileName.Length - 60));
                }

                SignRequest signRequestBody = new SignRequest()
                {
                    Contents = new List<Content>()
                        {
                            new Content()
                            {
                                ContentFormat        = ContentFormat.DIGEST,
                                Data                 = Convert.ToBase64String(bSecureHashResponse.DigestValue),
                                FileName             = fileName,
                                HashAlgorithm        = (ContentHashAlgorithm)Enum.Parse(typeof(ContentHashAlgorithm), process.DigestMethod.ToString()),
                                SignatureType        = ContentSignatureType.SIGNATURE,
                                ToBeArchived         = false,
                                ConfirmText          = process.FileName,
                                PadesVisualSignature = null
                            }
                        },
                    IsLogin = false,
                    Payer = SignRequestPayer.CLIENT,
                    RelyingPartyCallbackId = Guid.NewGuid().ToString()
                };

                //4. Изпращаме заявката за подписване.
                SendSignResponse signResponse = await btrustRemoteClient.V2SignPostAsync(
                    accept_language: "en"
                    , body: signRequestBody
                    , relyingPartyID: _signModuleOptions.CurrentValue.SIGN_BTRUST_REMOTE_RELYINGPARTY_ID
                    , rpToClientAuthorization: rpToClientAuthorization
                    , cancellationToken: cancellationToken);

                if (signResponse == null || signResponse.Data == null || string.IsNullOrEmpty(signResponse.Data.CallbackId))
                {
                    throw new NotSupportedException("Missing TransactionID from Btrust.");
                }

                //5. Вкарваме подписващия в статус "започнало отдалечено подписване", записваме сесиините му данни 
                //и данните за транзакцията в системата доставчик на отдалечено подписване 
                RemoteSignRequestAdditionalData signRequestAdditionalData = new RemoteSignRequestAdditionalData()
                {
                    TransactionID = signResponse.Data.CallbackId,
                    DigestTime = bSecureHashResponse.DigestTime,
                    UserCert = base64ClientCert,
                    RelyingPartyCallbackId = signRequestBody.RelyingPartyCallbackId
                };

                OperationResult signerStartSigningResult = await _signingProcessesService.SignerStartRemoteSigningAsync(processID, SignerID, SigningChannels.BtrustRemote, signRequestAdditionalData, cancellationToken);

                return signerStartSigningResult;
            }
            catch (BSecureDSSL.SwaggerException ex1)
            {
                _logger.LogException(ex1);

                if (ex1.StatusCode == 500 && ex1 is BSecureDSSL.SwaggerException<InternalServerErrorResponseDto> genericEx)
                {
                    if (string.Compare(genericEx.Result.Code, "OTHER_ERROR", true) == 0
                        && string.Compare(genericEx.Result.Message, "This certificate is not in its validity period in the moment of signing.", true) == 0)
                    {
                        return new OperationResult("GL_SIGN_EXPIRED_CERTIFICATE_DATE_E ", "GL_SIGN_EXPIRED_CERTIFICATE_DATE_E ");
                    }
                }

                //Грешка при подписване. Моля, свържете се с администратор на Портала на Агенция по вписванията.
                //След отстраняване на проблема, опитайте да подпишете отново.
                return new OperationResult("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            catch (BtrustRemoteClient.ApiException ex2)
            {
                _logger.LogException(ex2);

                return new OperationResult(ConvertBtrustRemoteClientExceptionToIError(ex2));
            }
            catch (Exception ex3)
            {
                _logger.LogException(ex3);

                return new OperationResult("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            finally
            {
                if(process != null && process.Content != null)
                {
                    process.Content.Dispose();
                    process.Content = null;
                }
            }
        }

        public async Task<OperationResult<BtrustPullingResult>> TryCompleteRemoteSigning(Guid processID, long SignerID, CancellationToken cancellationToken)
        {
            SigningProcess process = null;

            try
            {
                process = (await _signingProcessesService.SearchAsync(new SigningProcessesSearchCriteria()
                {
                    ProcessID = processID,
                    LoadSigners = true,
                    LoadContent = true
                }, cancellationToken)).SingleOrDefault();

                if (process == null)
                {
                    return new OperationResult<BtrustPullingResult>(OperationResultTypes.SuccessfullyCompleted) { Result = new BtrustPullingResult() { Code = "OK", Status = BtrustDocStatus.SIGNED } };
                }

                Signer currentSigner = process.Signers.Single(s => s.SignerID == SignerID);

                if (currentSigner.Status != SignerSigningStatuses.StartSigning)
                {
                    return new OperationResult<BtrustPullingResult>(OperationResultTypes.SuccessfullyCompleted) { Result = new BtrustPullingResult() { Code = "NOK" } };
                }

                if (process.Signers.Count(s => s.Status == SignerSigningStatuses.StartSigning) > 1 /* Подписва само един другите чакат. */
                    || currentSigner.SigningChannel != SigningChannels.BtrustRemote
                    || currentSigner.SigningAdditionalData == null)
                    return new OperationResult<BtrustPullingResult>(OperationResultTypes.SuccessfullyCompleted) { Result = new BtrustPullingResult() { Code = "NOK" } };

                //var additionalData = EAUJsonSerializer.Deserialize<RemoteSignRequestAdditionalData>(currentSigner.SigningAdditionalData);
                var signedContentResponse = await _btrustRemoteClientFactory.GetBtrustRemoteClient().V2SignGetAsync("en", currentSigner.TransactionID, _signModuleOptions.CurrentValue.SIGN_BTRUST_REMOTE_RELYINGPARTY_ID, cancellationToken);

                if (signedContentResponse.Code.ToUpper() == "COMPLETED")
                {
                    var signedContentData = signedContentResponse.Data.Signatures.ElementAt(0);

                    if (signedContentData.Status == SignatureResponseStatus.RECEIVED
                        || signedContentData.Status == SignatureResponseStatus.SIGNED)
                    {
                        OperationResult signerSignRes = null;
                        //Статус RECEIVED, защото току що сме свалили документа. Статус SIGNED е когато документа е подписан но все още не е свален.
                        using (Stream assembledDoc = await _documentSigningUtilityService.AssembleDocumentWithSignatureAsync(process.ContentType
                                                                                                , process.FileName
                                                                                                , process.Content
                                                                                                , process.DigestMethod.Value.ToString()
                                                                                                , process.Format.Value
                                                                                                , process.Type.Value.ToString()
                                                                                                , process.Level.Value
                                                                                                , currentSigner.SigningAdditionalData.UserCert
                                                                                                , signedContentData.Signature
                                                                                                , currentSigner.SigningAdditionalData.DigestTime.Value
                                                                                                , process.AdditionalData.SignatureXpath
                                                                                                , process.AdditionalData.SignatureXPathNamespaces
                                                                                                , process.Format == SigningFormats.PAdES))
                        {
                            //Това се прави за оптимизация.
                            if (process.Content != null)
                            {
                                process.Content.Dispose();
                                process.Content = null;
                            }

                            signerSignRes = await _signingProcessesService.SignerCompleteRemoteSigningAsync(processID, SignerID, assembledDoc, cancellationToken);
                        }

                        if (signerSignRes.IsSuccessfullyCompleted)
                        {
                            BtrustPullingResult resData = new BtrustPullingResult()
                            {
                                Code = "OK",
                                Status = BtrustDocStatus.SIGNED
                            };

                            return new OperationResult<BtrustPullingResult>(OperationResultTypes.SuccessfullyCompleted) { Result = resData };
                        }
                        else
                        {
                            var signerRejectRes = await _signingProcessesService.SignerRejectRemoteSigningAsync(processID, SignerID, signerSignRes.Errors.ElementAt(0).Message, cancellationToken);

                            if (!signerRejectRes.IsSuccessfullyCompleted)
                                throw new NotSupportedException(signerRejectRes.Errors.ElementAt(0).Message);
                        }
                    }
                    else
                    {
                        string reson = null;
                        switch (signedContentData.Status)
                        {
                            case SignatureResponseStatus.REJECTED:
                                reson = "GL_SIGN_REJECTED_E"; //Подписващият е отказал да подпише документа.
                                break;
                            case SignatureResponseStatus.EXPIRED:
                                reson = "GL_SIGN_BTRUST_EXPIRED_E"; //Документът не е подписан, поради изтичане на определеният за целта срок.
                                break;
                            case SignatureResponseStatus.ERROR:
                            case SignatureResponseStatus.REMOVED:
                                reson = "GL_SIGN_BTRUST_ERROR_E"; //Грешка при подписване e установена от B-Trust. Моля, свържете се с тях за изясняване на проблема или се опитайте да подпишете отново.
                                break;
                            default:
                                reson = "GL_SIGN_FAIL_E";
                                break;
                        }

                        var signerRejectRes = await _signingProcessesService.SignerRejectRemoteSigningAsync(processID, SignerID, reson, cancellationToken);

                        if (signerRejectRes.IsSuccessfullyCompleted)
                        {
                            BtrustPullingResult resData = new BtrustPullingResult()
                            {
                                Code = "OK",
                                Status = BtrustDocStatus.REJECTED,
                                RejectReson = reson
                            };

                            return new OperationResult<BtrustPullingResult>(OperationResultTypes.SuccessfullyCompleted) { Result = resData };
                        }
                    }
                }

                return new OperationResult<BtrustPullingResult>(OperationResultTypes.SuccessfullyCompleted) { Result = new BtrustPullingResult() { Code = "NOK" } };
            }
            catch (BSecureDSSL.SwaggerException ex1)
            {
                _logger.LogException(ex1);
                //Грешка при подписване. Моля, свържете се с администратор на Портала на Агенция по вписванията.
                //След отстраняване на проблема, опитайте да подпишете отново.
                return new OperationResult<BtrustPullingResult>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            catch (BtrustRemoteClient.ApiException ex2)
            {
                _logger.LogException(ex2);

                return new OperationResult<BtrustPullingResult>(ConvertBtrustRemoteClientExceptionToIError(ex2));
            }
            catch (Exception ex3)
            {
                _logger.LogException(ex3);

                //Грешка при подписване. Моля, свържете се с администратор на Портала на Агенция по вписванията.
                //След отстраняване на проблема, опитайте да подпишете отново.
                return new OperationResult<BtrustPullingResult>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            finally
            {
                if (process != null && process.Content != null)
                {
                    process.Content.Dispose();
                    process.Content = null;
                }
            }
        }

        #endregion

        #endregion

        #region Helpers

        private BissSignRequest generateBissSignRequest(byte[] digestValue, string userBase64Cert, string docConfirmText)
        {
            byte[] signedDigestValueHash = null;
            byte[] digestValueHash = null;
            BissSignRequest resultObj = null;

            #region Подписваме върнатия хеш

            //Локален сертификат, с който се подписва хеша така, че локалното BISS, да е сигурно че хеша не е подменен по време на кумуникацията.
            using (X509Certificate2 cert = _documentSigningUtilityService.GeServerCert())
            {
                //Генерираме Хеш на DigestValue.
                using (var crypt = new SHA256Managed())
                {
                    digestValueHash = crypt.ComputeHash(digestValue);
                }

                using (RSA rsa = cert.GetRSAPrivateKey())
                {
                    signedDigestValueHash = rsa.SignData(digestValueHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }

                string content = Convert.ToBase64String(digestValue);
                string signedContent = Convert.ToBase64String(signedDigestValueHash);
                string signedContentCert = Convert.ToBase64String(cert.RawData);

                resultObj = new BissSignRequest()
                {
                    Version              = "2.20",
                    SignatureType        = "signature",
                    ContentType          = "digest",
                    HashAlgorithm        = "SHA256",
                    Contents             = new string[]    { content           },
                    SignedContents       = new string[]    { signedContent     },
                    SignedContentsCert   = new string[]    { signedContentCert },
                    SignerCertificateB64 = userBase64Cert,
                    AdditonalConfirmText = docConfirmText,
                    ConfirmText          = new string[]    { "hash"            }
                };
            }

            #endregion

            return resultObj;
        }

        private IErrorCollection ConvertBtrustRemoteClientExceptionToIError(BtrustRemoteClient.ApiException ex)
        {
            var errors = new ErrorCollection();

            switch (ex.StatusCode)
            {
                case 206:
                    //Има файлове, чакащи за подпис
                    errors.Add(new TextError("GL_SIGN_BTRUST_ERR_FILESWAITING_E", "GL_SIGN_BTRUST_ERR_FILESWAITING_E"));
                    break;
                case 400:
                    //Изтекъл ауторизационен код или грешен идентификатор.
                    errors.Add(new TextError("GL_SIGN_INVLD_BTRUST_INPT_PARAM_E", "GL_SIGN_INVLD_BTRUST_INPT_PARAM_E"));
                    break;
                case 401:
                    //Неразрешен достъп.
                    errors.Add(new TextError("GL_SIGN_INVLD_BTRUST_INPT_PARAM_E", "GL_SIGN_INVLD_BTRUST_INPT_PARAM_E"));
                    break;
                case 404:
                    //Неразрешен достъп.
                    errors.Add(new TextError("GL_SIGN_MISSING_CERT_BTRUST_E", "GL_SIGN_MISSING_CERT_BTRUST_E"));
                    break;
                default:
                    errors.Add(new TextError("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E"));
                    break;
            }

            return errors;
        }

        private async Task<OperationResult<BissSignRequestExtended>> CreateBissSignRequestInternal(SigningProcess process, string Base64SigningCert)
        {
            try
            {
                DigestResponseDto digestResponseDto = await _documentSigningUtilityService.CreateDocumentHashAsync(
                    process.ContentType
                    , process.FileName
                    , process.Content
                    , process.DigestMethod.Value.ToString()
                    , process.Format.Value
                    , process.Type.Value.ToString()
                    , process.Level.Value
                    , Base64SigningCert
                    , process.AdditionalData.SignatureXpath
                    , process.AdditionalData.SignatureXPathNamespaces
                    , process.Format == SigningFormats.PAdES);

                BissSignRequest bissSignRequest = generateBissSignRequest(digestResponseDto.DigestValue, Base64SigningCert, process.FileName);

                OperationResult<BissSignRequestExtended> result = new OperationResult<BissSignRequestExtended>(OperationResultTypes.SuccessfullyCompleted)
                {
                    Result = new BissSignRequestExtended()
                    {
                        SignRequest      = bissSignRequest,
                        DocumentHashTime = digestResponseDto.DigestTime == null ? new long[] { } : new long[] { digestResponseDto.DigestTime.Value }
                    }
                };

                return result;
            }
            catch (BSecureDSSL.SwaggerException ex1)
            {
                _logger.LogException(ex1);

                if (ex1.StatusCode == 500 && ex1 is BSecureDSSL.SwaggerException<InternalServerErrorResponseDto> genericEx)
                {
                    if (string.Compare(genericEx.Result.Code, "OTHER_ERROR", true) == 0
                        && string.Compare(genericEx.Result.Message, "This certificate is not in its validity period in the moment of signing.", true) == 0)
                    {
                        return new OperationResult<BissSignRequestExtended>("GL_SIGN_EXPIRED_CERTIFICATE_DATE_E ", "GL_SIGN_EXPIRED_CERTIFICATE_DATE_E ");
                    }
                }

                //Грешка при подписване. Моля, свържете се с администратор на Портала на Агенция по вписванията.
                //След отстраняване на проблема, опитайте да подпишете отново.
                return new OperationResult<BissSignRequestExtended>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                //Грешка при подписване. Моля, свържете се с администратор на Портала на Агенция по вписванията.
                //След отстраняване на проблема, опитайте да подпишете отново.
                return new OperationResult<BissSignRequestExtended>("GL_SIGN_FAIL_E", "GL_SIGN_FAIL_E");
            }
        }

        private async Task<OperationResult<CertificateByPersonalIdResponse>> CheckAndGetUserCertAsync(IBtrustRemoteClient btrustRemoteClient, BtrustUserInputRequest btrustUserInput, CancellationToken cancellationToken)
        {
            try
            {
                IdentificatorType identificatorType = btrustUserInput.InputType.Value switch
                {
                    BtrustUserInputTypes.EGN => IdentificatorType.EGN,
                    BtrustUserInputTypes.LNCH => IdentificatorType.LNC,
                    BtrustUserInputTypes.EMAIL => IdentificatorType.EMAIL,
                    BtrustUserInputTypes.PHONE => IdentificatorType.PHONE,
                    _ => throw new ArgumentException(nameof(btrustUserInput.InputType), $"Not expected direction value: {btrustUserInput.InputType.Value}")
                };

                var clientCertResponse = await btrustRemoteClient.V2CertIdentityAsync(
                    "en"
                    , identificatorType
                    , btrustUserInput.Input
                    , _signModuleOptions.CurrentValue.SIGN_BTRUST_REMOTE_RELYINGPARTY_ID
                    , cancellationToken);

                return new OperationResult<CertificateByPersonalIdResponse>(OperationResultTypes.SuccessfullyCompleted) { Result = clientCertResponse };
            }
            catch (BtrustRemoteClient.ApiException ex)
            {
                _logger.LogException(ex);

                var errCollection = ConvertBtrustRemoteClientExceptionToIError(ex);

                return new OperationResult<CertificateByPersonalIdResponse>(errCollection);
            }
        }

        #endregion
    }
}
