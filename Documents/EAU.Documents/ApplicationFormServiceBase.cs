using CNSys;
using EAU.Common;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.Nomenclatures;
using EAU.Security;
using EAU.Services.Nomenclatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.MOI.BDS.NRBLD.Models;
using WAIS.Integration.MOI.Core.BDS.NRBLD;
using WAIS.Integration.MOI.KAT.AND;
using WAIS.Integration.MOI.KAT.AND.Models;
using WAIS.Integration.RegiX;

namespace EAU.Documents
{
    public class ApplicationFormInitializationRequest : DocumentFormInitializationRequest
    {
        public ResponseNType ApplicantInfo { get; set; }

        public bool HasNonPaidSlip { get; set; }

        //TODO
        public object EntityData { get; set; }
    }

    public class ApplicationFormValidationRequest
    {
        public DocumentFormData FormData { get; set; }

        public ResponseNType ApplicantInfo { get; set; }

        public ResponseNType RecipientInfo { get; set; }
    }

    public abstract class ApplicationFormServiceBase<TDomain, TView> : DocumentFormServiceBase<TDomain, TView>
        where TDomain : IApplicationForm
        where TView : ApplicationFormVMBase
    {

        public ApplicationFormServiceBase(IServiceProvider serviceProvider) : base(serviceProvider) { }

        #region Abstract Methods

        protected abstract List<Domain.Models.ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest request);

        protected abstract List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest request);

        protected abstract List<Domain.Models.IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request);

        #endregion

        #region Override

        protected override List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> GetAttachedDocumentsInternal(TDomain formDoamin)
        {
            if (formDoamin.AttachedDocuments != null && formDoamin.AttachedDocuments.Count > 0)
            {
                var result = new List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)>();

                foreach (var attDoc in formDoamin.AttachedDocuments)
                {
                    result.Add((attDoc.FileContent, attDoc.Description, Guid.Parse(attDoc.UniqueIdentifier), attDoc.FileType, attDoc.FileName, int.TryParse(attDoc.TypeCode, out int attDocTypeCode) ? attDocTypeCode : (int?)null));
                }

                return result;
            }

            return base.GetAttachedDocumentsInternal(formDoamin);
        }

        protected async override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeDocumentFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
            {
                return result;
            }

            if (request.Mode != DocumentModes.SignDocument && request.Mode != DocumentModes.ViewDocument)
            {
                var userAccessor = GetService<IEAUUserAccessor>();

                if (userAccessor.User?.IsUserIdentifiable != true || !userAccessor.User.PersonIdentifierType.HasValue || userAccessor.User.PersonIdentifierType == PersonIdentifierTypes.Other)
                {
                    //REQ_PEAU_0229 Подаване на ново заявление за ЕАУ
                    //Няма ЕГН или ЛНЧ в подписа
                    //Неуспешна автентикация! За подаване на заявление трябва да се автентикирате в системата с КЕП или чрез е-Автентикация.
                    return new OperationResult("GL_00011_E", "GL_00011_E");
                }

                if (userAccessor.User.PersonIdentifierType == PersonIdentifierTypes.EGN)
                {
                    request.AdditionalData["applicantEGN"] = userAccessor.User.PersonIdentifier;
                }

                if (userAccessor.User.PersonIdentifierType == PersonIdentifierTypes.LNCh)
                {
                    request.AdditionalData["applicantLNCh"] = userAccessor.User.PersonIdentifier;
                }

                //TODO Read UIC FromCert

                var personInfoResponse = await GetService<INRBLDServicesClientFactory>().GetNRBLDServicesClient().GetPersonInfoAsync(
                    userAccessor.User.PersonIdentifier,
                    false,
                    cancellationToken);

                if (!personInfoResponse.IsSuccessfullyCompleted)
                {
                    var errors = new ErrorCollection();
                    errors.AddRange(personInfoResponse.Errors.Select(e => (IError)(new TextError(e.Code, e.Message))));

                    return new OperationResult(errors);
                }

                var initRequest = new ApplicationFormInitializationRequest()
                {
                    AdditionalData = request.AdditionalData,
                    Form = request.Form,
                    Mode = request.Mode,
                    ServiceID = request.ServiceID,
                    Signatures = request.Signatures,
                    ApplicantInfo = personInfoResponse.Response
                };

                var possibleAuthorQualities = GetPossibleAuthorQualities(initRequest);
                request.AdditionalData["possibleAuthorQualities"] = possibleAuthorQualities != null && possibleAuthorQualities.Count > 0 ? string.Join(",", possibleAuthorQualities.Select(q => (int)q)) : null;

                var possibleRecipientTypes = GetPossibleRecipientTypes(initRequest);
                request.AdditionalData["possibleRecipientTypes"] = possibleRecipientTypes != null && possibleRecipientTypes.Count > 0 ? string.Join(",", possibleRecipientTypes.Select(q => (int)q)) : null;

                var possibleRecipientIdentityDocumentTypes = GetPossibleRecipientIdentityDocumentTypes(initRequest);
                request.AdditionalData["possibleRecipientIdentityDocumentTypes"] = possibleRecipientIdentityDocumentTypes != null && possibleRecipientIdentityDocumentTypes.Count > 0 ? string.Join(",", possibleRecipientIdentityDocumentTypes.Select(q => (int)q)) : null;


                var driverLicense = initRequest.ApplicantInfo.Document?.FirstOrDefault(d => d.DocumentType.Type.Code == DocTypeCode.DriverLicense || d.DocumentType.Type.Code == DocTypeCode.DriverLicenseForForeigner);

                if (driverLicense != null)
                {
                    try
                    {
                        //Прихващане на грещка при опит за проверка за налични задължения в NRBLD
                        var nrbldResponse = await GetService<IANDServicesClientFactory>().GetANDServicesClient().GetAllNRBLDObligationDocumentsByPersonAsync(
                            userAccessor?.RemoteIpAddress?.ToString(), null, userAccessor?.User?.PersonIdentifier, driverLicense.Number, cancellationToken);

                        if (nrbldResponse.IsSuccessfullyCompleted && nrbldResponse.Response?.allObligations?.Length > 0)
                        {
                            initRequest.HasNonPaidSlip = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = GetService<ILogger<ApplicationFormServiceBase<TDomain, TView>>>();

                        logger.LogError("Error during call AND NRBLD to check for obligations: " + ex);
                    }

                    // Ако вече са открити налични задължения в NRBLD, не е необходимо да проверяваме и за задължения в КАТ
                    if (!initRequest.HasNonPaidSlip) 
                    {
                        try
                        {
                            //Прихващане на грещка при опит за проверка за налични задължения в КАТ
                            var katResponse = await GetService<IANDServicesClientFactory>().GetANDServicesClient().GetAllObligationDocumentsByLicenceNumAsync(new ObligationDocumentsByLicenceNumRequest()
                            {
                                egn = userAccessor.User.PersonIdentifier,
                                licenceNum = driverLicense.Number,
                            }, cancellationToken);

                            if (katResponse.IsSuccessfullyCompleted && katResponse.Response?.allObligations?.Length > 0)
                            {
                                initRequest.HasNonPaidSlip = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            var logger = GetService<ILogger<ApplicationFormServiceBase<TDomain, TView>>>();

                            logger.LogError("Error during call AND KAT to check for obligations: " + ex);
                        }
                    }
                }

                result = await InitializeApplicationFormInternalAsync(initRequest, cancellationToken);

                if (!result.IsSuccessfullyCompleted)
                {
                    return result;
                }
            }

            return result;
        }

        protected async virtual Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken, bool skipBGDocumentValidation = false, bool skipValidation = false)
        {
            var isRemovingIrregularitiesInstruction = request.AdditionalData.ContainsKey("removingIrregularitiesInstructionURI");

            if (!skipValidation)
            {
                var vaidationResult = ValidateApplicant(request.ApplicantInfo, request.AdditionalData.ContainsKey("applicantEGN")
                    ? request.AdditionalData["applicantEGN"]
                    : request.AdditionalData["applicantLNCh"], skipBGDocumentValidation);

                if (vaidationResult.HasErrors)
                {
                    return new OperationResult(vaidationResult);
                }
            }

            var app = (TView)request.Form;

            if (request.Mode == DocumentModes.WithdrawService)
            {
                InitAppForFirstRegHeader(request);

                var appForm = (TView)request.Form;
                appForm.ElectronicAdministrativeServiceHeader.ApplicationType = Domain.Models.ApplicationType.AppForWithdrawService;

                InitApplicationRecipientWithdrawService(request);
            }
            else if (!isRemovingIrregularitiesInstruction && !request.AdditionalData.ContainsKey("additionalApplicationURI"))
            {
                InitAppForFirstRegHeader(request);

                InitElectronicServiceApplicant(request);

                InitDeclarations(request);

                await InitServiceTermTypeAndApplicantReceiptAsync(request, cancellationToken);
            }
            else
            {
                var userAccessor = GetService<IEAUUserAccessor>();

                if (app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData.Identifier.Item != userAccessor.User.PersonIdentifier)
                {
                    //Данните в секция "Заявител на услугата" от иницииращото заявление не съответстват на тези от текущата Ви автентикация в портала. За да продължите с подаване на заявлението трябва да влезете отново в портала, като използвате старата си автентикация.
                    return new OperationResult("GL_00029_E", "GL_00029_E");
                }

                if (isRemovingIrregularitiesInstruction) //Нередовности
                {
                    InitDeclarations(request);

                    InitAppForRemoveInvalidDataHeader(request);
                }
                else //Допълнително заявление
                {
                    app.ElectronicAdministrativeServiceHeader.ApplicationType = Domain.Models.ApplicationType.AppForChangeData;
                }

                InitApplicationAuthor(request);

                if (app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData != null &&
                    app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item == app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData.Identifier.Item)
                {
                    app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = ElectronicServiceAuthorQualityType.Personal;

                    request.AdditionalData["recipientPIN"] = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
                }
                else
                {
                    var possibleAuthorQualities = GetPossibleAuthorQualities(request).Where(q => q != ElectronicServiceAuthorQualityType.Personal);

                    if (possibleAuthorQualities.Count() == 0)
                    {
                        //Избраното заявление може да се подава само в лично качество. Данните от идентификацията Ви не съвпадат с данните на получателя на услугата.
                        return new OperationResult("GL_00001_E", "GL_00001_E");
                    }

                    if (app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal)
                        app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = possibleAuthorQualities.First();

                    if (app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData != null)
                    {
                        request.AdditionalData["recipientPIN"] = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
                    }
                    else
                    {
                        request.AdditionalData["recipientPIN"] = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData.Identifier;
                    }
                }
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        protected virtual Task<OperationResult> InitializeApplicationFormInternalAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            return InitializeApplicationFormInternalAsync(request, cancellationToken, false);
        }

        protected override void PrepareDomainDocumentInternal(TDomain formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            if (AttachedDocuments != null && AttachedDocuments.Count > 0)
            {
                formDoamin.AttachedDocuments = new List<Domain.Models.AttachedDocument>();

                foreach (var doc in AttachedDocuments)
                {
                    formDoamin.AttachedDocuments.Add(new Domain.Models.AttachedDocument()
                    {
                        Description = doc.Description,
                        FileName = doc.FileName,
                        FileType = doc.MimeType,
                        UniqueIdentifier = doc.Guid.ToString(),
                        TypeCode = doc.DocumentTypeID?.ToString(),
                        FileContent = doc.Content
                    });
                }
            }

            formDoamin.ElectronicAdministrativeServiceFooter = new Domain.Models.ElectronicAdministrativeServiceFooter();
            formDoamin.ElectronicAdministrativeServiceFooter.XMLDigitalSignature = new Domain.Models.XMLDigitalSignature();
            formDoamin.ElectronicAdministrativeServiceFooter.ApplicationSigningTime = DateTime.Now;
        }

        protected async override Task<IErrorCollection> ValidateDocumentFormInternalAsync(DocumentFormData formData, CancellationToken cancellationToken)
        {
            var reuslt = await base.ValidateDocumentFormInternalAsync(formData, cancellationToken);

            if (reuslt.HasErrors)
                return reuslt;

            var app = (TView)formData.Form;
            var nrlbldService = GetService<INRBLDServicesClientFactory>().GetNRBLDServicesClient();

            var request = new ApplicationFormValidationRequest()
            {
                FormData = formData
            };

            var userAccessor = GetService<IEAUUserAccessor>();
            var applicantResult = await nrlbldService.GetPersonInfoAsync(userAccessor.User.PersonIdentifier, false, cancellationToken);

            //Ако има промяна в статус на заявителя в БДС
            if (!applicantResult.IsSuccessfullyCompleted)
            {
                return new ErrorCollection(new List<IError>() { new TextError(applicantResult.Errors[0].Code, applicantResult.Errors[0].Message) });
            }

            request.ApplicantInfo = applicantResult.Response;

            if (app.ElectronicServiceApplicant.RecipientGroup.Recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person
               && app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item != null
                )
            {
                var recipientResult = await nrlbldService.GetPersonInfoAsync(app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item, false, cancellationToken);

                //Ако има промяна в статус на получателя в БДС
                if (!recipientResult.IsSuccessfullyCompleted)
                {
                    return new ErrorCollection(new List<IError>() { new TextError(recipientResult.Errors[0].Code, recipientResult.Errors[0].Message) });
                }

                request.RecipientInfo = recipientResult.Response;
            }

            //Проверка дали няма промяна на данните на заявителя, чрез направомена промяна на JSON заявките
            var forciblyChangedValidation = ValidateIsApplicantDataForciblyChanged(applicantResult.Response, app);

            if (forciblyChangedValidation != null)
            {
                return forciblyChangedValidation;
            }

            return await ValidateApplicationFormInternalAsync(request, cancellationToken);
        }

        protected virtual Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken, bool skipBGDocumentValidation = false, bool skipRecipientValidation = false)
        {
            var application = (TView)request.FormData.Form;

            //REQ_PEAU_0256 Автоматизирани проверки при преминаване към подписване на заявление за ЕАУ
            var valResult = ValidateApplicant(request.ApplicantInfo, application.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData.Identifier.Item, skipBGDocumentValidation);

            if (application.ElectronicServiceApplicant.RecipientGroup.Recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person
                && application.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality != ElectronicServiceAuthorQualityType.Personal
                && !skipRecipientValidation)
            {
                string pid = "";

                if (application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData != null
                    && application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier != null
                    && !string.IsNullOrWhiteSpace(application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item))
                {
                    pid = application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
                }

                var resValResult = ValidateRecipient(request.RecipientInfo, pid, skipBGDocumentValidation);

                foreach (var err in resValResult)
                {
                    valResult.Add(err);
                }
            }

            var localizer = GetService<IStringLocalizer>();

            #region Валидация на декларациите.

            if (application.Declarations != null && application.Declarations.Declarations != null)
            {
                var services = GetService<IServices>();
                var appService = services.Search().Single(s => string.Compare(s.SunauServiceUri, application.ElectronicAdministrativeServiceHeader.SUNAUServiceURI, true) == 0);
                var requiredDeclarationCodes = appService.Declarations.Where(d => d.IsRquired.GetValueOrDefault()).Select(d => d.Code).ToList();
                var checkedDeclaration = application.Declarations.Declarations.Where(d => d.IsDeclarationFilled);

                foreach (string declarationCode in requiredDeclarationCodes)
                {
                    if (!checkedDeclaration.Any(d => string.Compare(d.Code, declarationCode, true) == 0))
                    {
                        if (string.Compare(declarationCode, "Policy_GDPR", true) == 0)
                        {
                            //Липсва задължителна политика за поверителност!
                            valResult.Add(new TextError("DOC_GL_DeclarationVM_MissingRequiredPolicy_E", "DOC_GL_DeclarationVM_MissingRequiredPolicy_E"));
                        }
                        else
                        {
                            //Липсва задължителна декларация!
                            if (!valResult.Any(e => string.Compare(e.Code, "DOC_GL_DeclarationVM_MissingRequiredDeclaration_E", true) == 0))
                            {
                                //Грешката се добавя само веднъж.
                                valResult.Add(new TextError("DOC_GL_DeclarationVM_MissingRequiredDeclaration_E", "DOC_GL_DeclarationVM_MissingRequiredDeclaration_E"));
                            }
                        }
                    }
                }

                var nomDeclarationsRequiredAdditionDescriptionCodes = appService.Declarations
                    .Where(d => d.IsAdditionalDescriptionRequired.GetValueOrDefault())
                    .Select(d => d.Code);

                if (checkedDeclaration.Any(chD => nomDeclarationsRequiredAdditionDescriptionCodes.Contains(chD.Code)
                     && string.IsNullOrEmpty(chD.FurtherDescriptionFromDeclarer)))
                {
                    //Липсва задължително допълнително описание към декларация/и.
                    valResult.Add(new TextError("DOC_GL_MissingRequiredDescriptionToDeclarations_E", "DOC_GL_MissingRequiredDescriptionToDeclarations_E"));
                }
            }

            #endregion

            //Валидация за неподписани шаблонни декларации.
            if (request.FormData.AttachedDocuments != null
                && request.FormData.AttachedDocuments.Any(ad => ad.Content == null && string.IsNullOrEmpty(ad.MimeType) && string.IsNullOrEmpty(ad.FileName)))
            {
                valResult.Add(new TextError("GL_UNSIGNED_ATTACHED_DOCUMENTS_GENERATED_BY_TEMPLATE_E", "GL_UNSIGNED_ATTACHED_DOCUMENTS_GENERATED_BY_TEMPLATE_E"));
            }

            return Task.FromResult(valResult);
        }

        protected virtual Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            return ValidateApplicationFormInternalAsync(request, cancellationToken, false, false);
        }

        #endregion

        #region Helpers

        private void InitAppForRemoveInvalidDataHeader(ApplicationFormInitializationRequest request)
        {
            var app = (TView)request.Form;

            app.ElectronicAdministrativeServiceHeader.ApplicationType = Domain.Models.ApplicationType.AppForRemoveInvalidData;

            var orgAppURI = new WAIS.Integration.EPortal.Models.URI(request.AdditionalData["originalApplicationURI"]);

            app.ElectronicAdministrativeServiceHeader.DocumentURI = new Domain.Models.DocumentURI()
            {
                RegisterIndex = orgAppURI.RegisterIndex,
                SequenceNumber = orgAppURI.SequenceNumber,
                ReceiptOrSigningDate = orgAppURI.ReceiptOrSigningDate
            };
        }

        private void InitAppForFirstRegHeader(ApplicationFormInitializationRequest request)
        {
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.ServiceID == request.ServiceID);
            var docTypes = GetService<IDocumentTypes>();
            var docType = docTypes.Search().Single(dt => dt.Uri == DocumentTypeUri);

            var app = (TView)request.Form;

            app.ElectronicAdministrativeServiceHeader = new ElectronicAdministrativeServiceHeaderVM();
            app.ElectronicAdministrativeServiceHeader.ApplicationType = Domain.Models.ApplicationType.AppForFirstReg;
            app.ElectronicAdministrativeServiceHeader.AdmStructureUnitName = service.AdmStructureUnitName;

            app.ElectronicAdministrativeServiceHeader.ElectronicServiceProviderBasicData = new Domain.Models.ElectronicServiceProviderBasicData();
            app.ElectronicAdministrativeServiceHeader.ElectronicServiceProviderBasicData.ElectronicServiceProviderType = Domain.Models.ElectronicServiceProviderType.Administration;

            var globalOptions = GetService<IOptionsMonitor<GlobalOptions>>().CurrentValue;

            app.ElectronicAdministrativeServiceHeader.ElectronicServiceProviderBasicData.EntityBasicData = new Domain.Models.EntityBasicData()
            {
                Identifier = globalOptions.GL_ADM_STRUCTURE_UIC,
                Name = globalOptions.GL_ADM_STRUCTURE_NAME
            };

            app.ElectronicAdministrativeServiceHeader.SUNAUServiceName = service.Name;
            app.ElectronicAdministrativeServiceHeader.SUNAUServiceURI = service.SunauServiceUri;
            app.ElectronicAdministrativeServiceHeader.DocumentTypeName = docType.Name;
            app.ElectronicAdministrativeServiceHeader.DocumentTypeURI = new Domain.Models.DocumentTypeURI()
            {
                RegisterIndex = Convert.ToInt32(docType.Uri.Split('-')[0]),
                BatchNumber = Convert.ToInt32(docType.Uri.Split('-')[1]),
            };
        }

        private void InitElectronicServiceApplicant(ApplicationFormInitializationRequest request)
        {
            var app = (TView)request.Form;

            app.ElectronicServiceApplicant = new ElectronicServiceApplicantVM()
            {
                RecipientGroup = new RecipientGroupVM()
                {
                    AuthorWithQuality = new AuthorWithQualityVM()
                    {
                        Author = new ElectronicStatementAuthorVM()
                    },
                    Recipient = new ElectronicServiceRecipientVM()
                }
            };

            InitApplicationAuthor(request);

            var possibleAuthorQualities = GetPossibleAuthorQualities(request);
            app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = possibleAuthorQualities.First();

            InitApplicationRecipient(request);
        }

        private void InitApplicationAuthor(ApplicationFormInitializationRequest request)
        {
            var app = (TView)request.Form;

            var author = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author;

            author.SelectedChoiceType = PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person;

            var userAccessor = GetService<IEAUUserAccessor>();

            if (string.IsNullOrWhiteSpace(author.EmailAddress))
                author.EmailAddress = userAccessor.User.Claims.GetEmail();

            author.ItemPersonBasicData = new PersonBasicDataVM()
            {
                Identifier = new Domain.Models.PersonIdentifier()
                {
                    Item = userAccessor.User.PersonIdentifier,
                    ItemElementName = userAccessor.User.PersonIdentifierType == PersonIdentifierTypes.EGN ? Domain.Models.PersonIdentifierChoiceType.EGN : Domain.Models.PersonIdentifierChoiceType.LNCh
                }
            };

            if (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null)
            {
                author.ItemPersonBasicData.Names = new Domain.Models.PersonNames()
                {
                    First = request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.Names.FirstName.Cyrillic,
                    Middle = request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.Names.Surname?.Cyrillic,
                    Last = request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.Names.Family.Cyrillic
                };
            }
            else
            {
                var firstName = request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF.Names.FirstName.Cyrillic.Split(' ')[0];
                var lastName = request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF.Names.FirstName.Cyrillic.Substring(firstName.Length).Trim();

                author.ItemPersonBasicData.Names = new Domain.Models.PersonNames()
                {
                    First = firstName,
                    Last = lastName
                };
            }

            if (request.ApplicantInfo.Document?.Count > 0)
                author.ItemPersonBasicData.IdentityDocument = new IdentityDocumentBasicDataVM()
                {
                    IdentityIssuer = request.ApplicantInfo.Document.First().Issuer,
                    IdentitityIssueDate = request.ApplicantInfo.Document.First().IssueDate,
                    IdentityDocumentType = GetIdentificationDocumentsTypeByDocTypeCode(request.ApplicantInfo.Document.First().DocumentType.Type.Code.Value),
                    IdentityNumber = request.ApplicantInfo.Document.First().Number
                };
        }

        private void InitApplicationRecipientWithdrawService(ApplicationFormInitializationRequest request)
        {
            var app = (TView)request.Form;

            InitApplicationAuthor(request);

            if (app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData != null &&
                app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item == app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData.Identifier.Item)
            {
                app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = ElectronicServiceAuthorQualityType.Personal;

                request.AdditionalData["recipientPIN"] = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
            }
            else
            {
                var possibleAuthorQualities = GetPossibleAuthorQualities(request).Where(q => q != ElectronicServiceAuthorQualityType.Personal);

                if (possibleAuthorQualities != null && possibleAuthorQualities.Count() > 0
                    && app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal)
                {
                    app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality = possibleAuthorQualities.First();
                }

                if (app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData != null)
                {
                    request.AdditionalData["recipientPIN"] = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData.Identifier.Item;
                }
                else
                {
                    request.AdditionalData["recipientPIN"] = app.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemEntityBasicData.Identifier;
                }
            }
        }

        private void InitApplicationRecipient(ApplicationFormInitializationRequest request)
        {
            var app = (TView)request.Form;
            var possibleRecipientTypes = GetPossibleRecipientTypes(request);

            var recipient = app.ElectronicServiceApplicant.RecipientGroup.Recipient;

            recipient.SelectedChoiceType = possibleRecipientTypes.First();
            var selectedAuthorQuality = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality;


            if (selectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal)
            {
                //За Personal вида на заявителя е само физическо лице.
                recipient.SelectedChoiceType = PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person;
                recipient.ItemPersonBasicData = app.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData;
            }
            else if (selectedAuthorQuality == ElectronicServiceAuthorQualityType.Representative)
            {
                if (recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person)
                {
                    recipient.ItemPersonBasicData = new PersonBasicDataVM();
                    InitPersonBasicData(recipient.ItemPersonBasicData);
                }
                else
                {
                    recipient.ItemEntityBasicData = new EntityBasicData();
                    InitEntityBasicData(recipient.ItemEntityBasicData);
                }
            }
            else if (selectedAuthorQuality == ElectronicServiceAuthorQualityType.LegalRepresentative)
            {
                //За LegalRepresentative вида на заявителя е само юридическо лице.
                recipient.SelectedChoiceType = PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Entity;
                recipient.ItemEntityBasicData = new EntityBasicData();
                InitEntityBasicData(recipient.ItemEntityBasicData);
            }
            else if (selectedAuthorQuality == ElectronicServiceAuthorQualityType.Official)
            {
                if (recipient.SelectedChoiceType == PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person)
                {
                    recipient.ItemPersonBasicData = new PersonBasicDataVM();
                    InitPersonBasicData(recipient.ItemPersonBasicData);
                }
                else
                {
                    recipient.ItemEntityBasicData = new EntityBasicData();
                    InitEntityBasicData(recipient.ItemEntityBasicData);
                }
            }
        }

        private void InitEntityBasicData(EntityBasicData entityBasicData)
        {
            var userAccessor = GetService<IEAUUserAccessor>();

            if (userAccessor.User != null && !string.IsNullOrWhiteSpace(userAccessor.User.UIC))
            {
                var regiXEntityData = GetService<IEntityDataServicesClientFactory>().GetEntityDataServicesClient().GetEntityDataAsync(userAccessor.User.UIC, CancellationToken.None).GetAwaiter().GetResult();

                if (regiXEntityData != null && regiXEntityData.IsSuccessfullyCompleted)
                {
                    entityBasicData.Identifier = regiXEntityData.Response.Identifier;
                    entityBasicData.Name = regiXEntityData.Response.Name;
                }
            }
        }

        private void InitPersonBasicData(PersonBasicDataVM personBasicData)
        {
            personBasicData.Identifier = new PersonIdentifier();
            personBasicData.Identifier.ItemElementName = PersonIdentifierChoiceType.EGN;
            personBasicData.IdentityDocument = new IdentityDocumentBasicDataVM();
            personBasicData.Names = new PersonNames();
        }

        private void InitDeclarations(ApplicationFormInitializationRequest request)
        {
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.ServiceID == request.ServiceID);

            var app = (TView)request.Form;

            if (app.Declarations == null || app.Declarations.Declarations == null || app.Declarations.Declarations.Count == 0)
            {
                app.Declarations = new DeclarationsVM();

                if (service.Declarations != null && service.Declarations.Count > 0)
                {
                    app.Declarations.Declarations = new List<DeclarationVM>();

                    foreach (var declaration in service.Declarations)
                    {
                        if (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF != null && string.Equals(declaration.Code, "DECL_LIVING_OUTSIDE_EU")) { 
                            continue; 
                        }

                        app.Declarations.Declarations.Add(new DeclarationVM()
                        {
                            Code = declaration.Code,
                            Content = declaration.Content,
                            IsDeclarationFilled = false
                        });
                    }
                }
            }
            else //Заявление за отстраняване на нередовностите
            {
                app.Declarations.Declarations.RemoveAll(d => !service.Declarations.Any(sd => sd.Code == d.Code));

                foreach (var declaration in service.Declarations)
                {
                    if (request.ApplicantInfo.PersonData.PersonIdentification.PersonIdentificationF != null && string.Equals(declaration.Code, "DECL_LIVING_OUTSIDE_EU")) { 
                        continue; 
                    }

                    var appDeclaration = app.Declarations.Declarations.SingleOrDefault(d => d.Code == declaration.Code);

                    if (appDeclaration == null)
                    {
                        app.Declarations.Declarations.Add(new DeclarationVM()
                        {
                            Code = declaration.Code,
                            Content = declaration.Content,
                            IsDeclarationFilled = false
                        });
                    }
                    else
                    {
                        appDeclaration.Content = declaration.Content;

                        if (appDeclaration.Code == "Policy_GDPR" ||
                            appDeclaration.Code == "DECL_ValidData")
                        {
                            appDeclaration.IsDeclarationFilled = false;
                        }
                    }
                }
            }
        }

        private async Task InitServiceTermTypeAndApplicantReceiptAsync(ApplicationFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var services = GetService<IServices>();
            var service = services.Search().Single(s => s.ServiceID == request.ServiceID);

            var app = (TView)request.Form;
            app.ServiceTermTypeAndApplicantReceipt = new ServiceTermTypeAndApplicantReceiptVM();

            app.ServiceTermTypeAndApplicantReceipt.ServiceTermType = (Domain.Models.ServiceTermType)(int)service.SeviceTerms.First().ServiceTermType.Value;
            app.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData = new ServiceApplicantReceiptDataVM();
            app.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod = (Domain.Models.ServiceResultReceiptMethods)service.DeliveryChannels.First().DeliveryChannelID;

            if (app.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.ServiceResultReceiptMethod == Domain.Models.ServiceResultReceiptMethods.UnitInAdministration)
            {
                var deliveryUnitsCache = GetService<IDeliveryUnitsInfo>();
                await deliveryUnitsCache.EnsureLoadedAsync(request.ServiceID.Value, cancellationToken);

                var deliveryUnits = deliveryUnitsCache.Search(request.ServiceID.Value, out DateTime? lastModified);

                if (deliveryUnits.Count() == 1)
                {
                    app.ServiceTermTypeAndApplicantReceipt.ServiceApplicantReceiptData.UnitInAdministration = new Domain.Models.ServiceApplicantReceiptDataUnitInAdministration()
                    {
                        EntityBasicData = app.ElectronicAdministrativeServiceHeader.ElectronicServiceProviderBasicData.EntityBasicData,
                        AdministrativeDepartmentCode = deliveryUnits.First().UnitID.ToString(),
                        AdministrativeDepartmentName = deliveryUnits.First().Name
                    };
                }
            }
        }

        /// <summary>
        /// Валидра по изискване REQ_PEAU_0229 Подаване на ново заявление за ЕАУ
        /// </summary>
        /// <param name="personInfo"></param>
        /// <returns></returns>
        private IErrorCollection ValidateApplicant(ResponseNType personInfo, string pid, bool skipBGDocumentValidation = false)
        {
            //Валидираме с валидациите за получателя и допълваме
            var errors = ValidateRecipient(personInfo, pid, skipBGDocumentValidation);

            if (string.IsNullOrWhiteSpace(pid))
                return errors;

            var localizer = GetService<IStringLocalizer>();

            if (personInfo.PersonData.Prohibition > 0)
            {
                //лицето е поставено под запрещение. - Тази грешка се локализира на сървара за това ключа и не трябва да отговаря на съществуващ, зада не се промени съобщението
                var localizedError = localizer["GL_00020_E"].Value.Replace("{pid}", pid);
                errors.Add(new TextError(localizedError, localizedError));
            }

            return errors;
        }

        /// <summary>
        /// Валидра по изискване REQ_PEAU_0098 Интеграция с НАИФ НРБЛД, във връзка със заявяване/предоставяне на ЕАУ
        /// </summary>
        /// <param name="personInfo"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private IErrorCollection ValidateRecipient(ResponseNType personInfo, string pid, bool skipBGDocumentValidation = false)
        {
            var errors = new ErrorCollection();

            if (string.IsNullOrWhiteSpace(pid))
                return errors;

            var localizer = GetService<IStringLocalizer>();

            if (personInfo == null)
            {
                //лице с посочения идентификатор не е намерено в системата. - Тази грешка се локализира на сървара за това ключа и не трябва да отговаря на съществуващ, зада не се промени съобщението
                var localizedError = localizer["GL_00016_E"].Value.Replace("{pid}", pid);
                errors.Add(new TextError(localizedError, localizedError));

                return errors;
            }

            if (personInfo.Address == null || !personInfo.Address.Any(a => a.id == AddresType.CurrentАddress || a.id == AddresType.PermanentАddress))
            {
                //лицето няма актуален постоянен/настоящ адрес в Република България. - Тази грешка се локализира на сървара за това ключа и не трябва да отговаря на съществуващ, зада не се промени съобщението
                var localizedError = localizer["GL_00017_E"].Value.Replace("{pid}", pid);
                errors.Add(new TextError(localizedError, localizedError));
            }

            if (!skipBGDocumentValidation)
            {
                //За валиден български личен документ приемаме всичко, което се върне от направената справка с изключение на шофьорската книжка.
                var validBulgarianDocuments = personInfo.Document.Where(d => d.DocumentType.Type.Code != DocTypeCode.DriverLicenseForForeigner
                && d.DocumentType.Type.Code != DocTypeCode.DriverLicense && d.CurrentStatus.StatusType.Code == DocumentStatusTypeCode.Valid).ToList();

                if (validBulgarianDocuments == null || validBulgarianDocuments.Count == 0)
                {
                    //лицето не притежава валиден български личен документ. - Тази грешка се локализира на сървара за това ключа и не трябва да отговаря на съществуващ, зада не се промени съобщението
                    var localizedError = localizer["GL_00018_E"].Value.Replace("{pid}", pid);
                    errors.Add(new TextError(localizedError, localizedError));
                }
            }

            if (personInfo.PersonData.PersonIdentification.PersonIdentificationF != null
                && (personInfo.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value == StatutName.EUCitizenWithoutResidencePermit
                || personInfo.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value == StatutName.ForeignerPermanentlyWithoutResidencePermit
                || personInfo.PersonData.PersonIdentification.PersonIdentificationF.Statut.Value == StatutName.ForeignerTemporarilyWithoutResidencePermit))
            {
                //Не можете да продължите със заявяване на избраната от Вас услуга. В Националния автоматизиран информационен фонд "Национален регистър на българските лични документи" лицето с ЕГН / ЛНЧ / ЛН { pid}
                //няма разрешено пребиваване в Република България.
                var localizedError = localizer["GL_00039_E"].Value.Replace("{pid}", pid);
                errors.Add(new TextError(localizedError, localizedError));
            }

            //Дата в формата ddmmgggg, 00mmgggg, 0000gggg
            var birthDate = personInfo.PersonData.PersonIdentification.PersonIdentificationBG != null ?
                personInfo.PersonData.PersonIdentification.PersonIdentificationBG.BirthDate :
                DateTime.ParseExact(personInfo.PersonData.PersonIdentification.PersonIdentificationF.BirthDate.StartsWith("0000") ?
                    "0101" + personInfo.PersonData.PersonIdentification.PersonIdentificationF.BirthDate.Substring(4) : personInfo.PersonData.PersonIdentification.PersonIdentificationF.BirthDate.StartsWith("00") ?
                    "01" + personInfo.PersonData.PersonIdentification.PersonIdentificationF.BirthDate.Substring(2) :
                    personInfo.PersonData.PersonIdentification.PersonIdentificationF.BirthDate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (DateTime.Now.Year - birthDate.Year < 18 ||
                (DateTime.Now.Year - birthDate.Year == 18 && DateTime.Now.Month - birthDate.Month < 0) ||
                (DateTime.Now.Year - birthDate.Year == 18 && DateTime.Now.Month - birthDate.Month == 0 && DateTime.Now.Day - birthDate.Day <= 0))
            {
                //лицето не е навършило 18 години.
                var localizedError = localizer["GL_00019_E"].Value.Replace("{pid}", pid);
                errors.Add(new TextError(localizedError, localizedError));
            }

            if (personInfo.PersonData.DeathDate.HasValue)
            {
                //лицето е починало. - Тази грешка се локализира на сървара за това ключа и не трябва да отговаря на съществуващ, зада не се промени съобщението
                var localizedError = localizer["GL_00021_E"].Value.Replace("{pid}", pid);
                errors.Add(new TextError(localizedError, localizedError));
            }

            return errors;
        }

        private Domain.Models.IdentityDocumentType GetIdentificationDocumentsTypeByDocTypeCode(DocTypeCode docTypeCode)
        {
            switch (docTypeCode)
            {
                case DocTypeCode.IdentityCard:
                    return Domain.Models.IdentityDocumentType.PersonalCard;
                case DocTypeCode.Passport:
                    return Domain.Models.IdentityDocumentType.Passport;
                case DocTypeCode.DriverLicense:
                    return Domain.Models.IdentityDocumentType.DrivingLicense;
                case DocTypeCode.DriverLicenseForForeigner:
                    return Domain.Models.IdentityDocumentType.DrivingLicense;
                case DocTypeCode.CardForContiniouslyStayingForeigner:
                    return Domain.Models.IdentityDocumentType.ResidencePermitForAContinuouslyStayingForeignerInBulgaria;
                case DocTypeCode.CardForPermanentStayingForeigner:
                    return Domain.Models.IdentityDocumentType.ResidencePermitForPermanentResidenceInBulgariaForeigner;
                case DocTypeCode.CertificateForTravelingAbroadForAPersonWithoutCitizenship:
                    return Domain.Models.IdentityDocumentType.CertificateForTravelingAbroadForAPersonWithoutCitizenship;
                case DocTypeCode.CertificateForTravelingAbroadOfAForeignerGrantedAsylum:
                    return Domain.Models.IdentityDocumentType.CertificateForTravelingAbroadOfAForeignerGrantedAsylum;
                case DocTypeCode.CertificateForTravelingAbroadOfAForeignerWithHumanitarianStatus:
                    return Domain.Models.IdentityDocumentType.CertificateForTravelingAbroadOfAForeignerWithHumanitarianStatus;
                case DocTypeCode.CertificateForTravelingAbroadOfRefugee:
                    return Domain.Models.IdentityDocumentType.CertificateForTravelingAbroadOfRefugee;
                case DocTypeCode.ConsularCard:
                    return Domain.Models.IdentityDocumentType.ConsularCard;
                case DocTypeCode.DiplomaticCard:
                    return Domain.Models.IdentityDocumentType.DiplomaticCard;
                case DocTypeCode.DiplomaticPassport:
                    return Domain.Models.IdentityDocumentType.DiplomaticPassport;
                case DocTypeCode.MapForeignerGrantedAsylum:
                    return Domain.Models.IdentityDocumentType.MapForeignerGrantedAsylum;
                case DocTypeCode.MapOfForeignerWithHumanitarianStatus:
                    return Domain.Models.IdentityDocumentType.MapOfForeignerWithHumanitarianStatus;
                case DocTypeCode.MapOfStaff:
                    return Domain.Models.IdentityDocumentType.MapOfStaff;
                case DocTypeCode.MapOfTheAdministrativeAndTechnicalStaff:
                    return Domain.Models.IdentityDocumentType.MapOfTheAdministrativeAndTechnicalStaff;
                case DocTypeCode.OfficialPassport:
                    return Domain.Models.IdentityDocumentType.OfficialPassport;
                case DocTypeCode.PassportForForeigner:
                    return Domain.Models.IdentityDocumentType.Passport;
                case DocTypeCode.RefugeeCard:
                    return Domain.Models.IdentityDocumentType.RefugeeCard;
                case DocTypeCode.ResidenceCertificateForEUCitizens:
                    return Domain.Models.IdentityDocumentType.ResidenceCertificateForEUCitizens;
                case DocTypeCode.ResidencePermit:
                    return Domain.Models.IdentityDocumentType.ResidencePermit;
                case DocTypeCode.ResidencePermitForResidentFamilyMemberOfEUCitizenWhoHasNotExercised:
                    return Domain.Models.IdentityDocumentType.ResidencePermitForResidentFamilyMemberOfEUCitizenWhoHasNotExercised;
                case DocTypeCode.SeaManPassport:
                    return Domain.Models.IdentityDocumentType.SeaManPassport;
                case DocTypeCode.TemporaryCardOfForeigner:
                    return Domain.Models.IdentityDocumentType.TemporaryCardOfForeigner;
                case DocTypeCode.TemporaryCardOfRefugee:
                    return Domain.Models.IdentityDocumentType.RefugeeCard;
                case DocTypeCode.TemporaryCertificateForLeavingTheRepublicOfBulgaria:
                    return Domain.Models.IdentityDocumentType.TemporaryCertificateForLeavingTheRepublicOfBulgaria;
                case DocTypeCode.TemporaryCertificateForRefugee:
                    return Domain.Models.IdentityDocumentType.RefugeeCard;
                case DocTypeCode.TemporaryCertificateForRefugeeFastOrder:
                    return Domain.Models.IdentityDocumentType.RefugeeCard;
                default:
                    throw new ArgumentException("There is no MOIBIDPersonalIdentificationDocumentType for this DocTypeCode!");
            }
        }

        private IErrorCollection ValidateIsApplicantDataForciblyChanged(ResponseNType applicantInfo, TView application)
        {
            string applicantPID, applicantFirstName, applicantSurnameName, applicantFamilyName;
            string applicantLNCh = null;
            var author = application.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.Author.ItemPersonBasicData;

            if (applicantInfo.PersonData.PersonIdentification.PersonIdentificationBG != null)
            {
                applicantPID = applicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.PIN;
                applicantFirstName = applicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.Names.FirstName?.Cyrillic;
                applicantSurnameName = applicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.Names.Surname?.Cyrillic;
                applicantFamilyName = applicantInfo.PersonData.PersonIdentification.PersonIdentificationBG.Names.Family?.Cyrillic;
            }
            else
            {
                applicantPID = applicantInfo.PersonData.PersonIdentification.PersonIdentificationF.PIN;
                applicantLNCh = applicantInfo.PersonData.PersonIdentification.PersonIdentificationF.LNC;
                applicantFirstName = applicantInfo.PersonData.PersonIdentification.PersonIdentificationF.Names.FirstName?.Cyrillic.Split(' ')[0];
                applicantSurnameName = null;
                applicantFamilyName = applicantInfo.PersonData.PersonIdentification.PersonIdentificationF.Names.FirstName?.Cyrillic.Substring(applicantFirstName.Length).Trim();
            }

            if (!((author.Identifier.Item == applicantPID || author.Identifier.Item == applicantLNCh) &&
                  (string.IsNullOrEmpty(author.Names.First) && string.IsNullOrEmpty(applicantFirstName) || author.Names.First == applicantFirstName) &&
                  (string.IsNullOrEmpty(author.Names.Middle) && string.IsNullOrEmpty(applicantSurnameName) || author.Names.Middle == applicantSurnameName) &&
                  (string.IsNullOrEmpty(author.Names.Last) && string.IsNullOrEmpty(applicantFamilyName) || author.Names.Last == applicantFamilyName)))
            {
                //Имената и/ или идентификатора (ЕГН/ЛНЧ/ЛН) на лицето заявител не съвпадат с данните от Националния автоматизиран информационен фонд "Национален регистър на българските лични документи".
                return new ErrorCollection(new List<IError> { new TextError("GL_00035_E", "GL_00035_E") });
            }

            if (application.ElectronicServiceApplicant.RecipientGroup.AuthorWithQuality.SelectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal)
            {
                var recipient = application.ElectronicServiceApplicant.RecipientGroup.Recipient.ItemPersonBasicData;

                if (!(author.Identifier.Item == recipient.Identifier.Item &&
                  (string.IsNullOrEmpty(author.Names.First) && string.IsNullOrEmpty(recipient.Names.First) || author.Names.First == recipient.Names.First) &&
                  (string.IsNullOrEmpty(author.Names.Middle) && string.IsNullOrEmpty(recipient.Names.Middle) || author.Names.Middle == recipient.Names.Middle) &&
                  (string.IsNullOrEmpty(author.Names.Last) && string.IsNullOrEmpty(recipient.Names.Last) || author.Names.Last == recipient.Names.Last) &&
                  ((author.IdentityDocument == null && recipient.IdentityDocument == null) ||
                   (author.IdentityDocument?.IdentityNumber == recipient.IdentityDocument?.IdentityNumber && author.IdentityDocument?.IdentityDocumentType == recipient.IdentityDocument?.IdentityDocumentType && author.IdentityDocument?.IdentityIssuer == recipient.IdentityDocument?.IdentityIssuer && author.IdentityDocument?.IdentitityIssueDate == recipient.IdentityDocument?.IdentitityIssueDate))))
                {
                    //При заявяване на услугата в лично качество, данните на заявителя и получателя на услугата трябва да съвпадат.
                    return new ErrorCollection(new List<IError> { new TextError("GL_00036_E", "GL_00036_E") });
                }
            }

            if (author.IdentityDocument != null)
            {
                var applicantIdentityDocument = applicantInfo.Document?.FirstOrDefault(d => d.Number == author.IdentityDocument.IdentityNumber);

                if (applicantIdentityDocument == null ||
                   applicantIdentityDocument.IssueDate != author.IdentityDocument.IdentitityIssueDate ||
                   applicantIdentityDocument.Issuer != author.IdentityDocument.IdentityIssuer)
                {
                    //Попълнените в заявлението данни за личен документ на заявителя на услугата не съвпадат с данните за документа от Националния автоматизиран информационен фонд "Национален регистър на българските лични документи".
                    return new ErrorCollection(new List<IError> { new TextError("GL_00037_E", "GL_00037_E") });
                }
            }

            return null;
        }

        public PersonAddress getPersonAdress(ResponseNType personInfo, AddresType[] adrTypes)
        {
            PersonAddress address = null;
            Address bdsAddress = null;

            foreach (var adrType in adrTypes)
            {
                foreach (var adr in personInfo.Address)
                {
                    if (adr.id == adrType)
                    {
                        bdsAddress = adr;
                        break;
                    }
                }
                if (bdsAddress != null)
                {
                    break;
                }
            }


            if (bdsAddress != null)
            {
                address = new PersonAddress();

                if (bdsAddress.District != null)
                {
                    address.DistrictGRAOCode = bdsAddress.District.Code?.PadLeft(2, '0');
                    address.DistrictGRAOName = bdsAddress.District.Value;
                }

                if (bdsAddress.Municipality != null)
                {
                    address.MunicipalityGRAOCode = bdsAddress.Municipality.Code?.PadLeft(2, '0');
                    address.MunicipalityGRAOName = bdsAddress.Municipality.Value;
                }

                if (bdsAddress.Settlement != null)
                {
                    address.SettlementGRAOCode = bdsAddress.Settlement.Code?.PadLeft(5, '0');
                    address.SettlementGRAOName = bdsAddress.Settlement.Value;
                }

                if (bdsAddress.Location != null)
                {
                    address.StreetText = bdsAddress.Location.Value;
                }

                address.Apartment = bdsAddress.Apartment;
                address.BuildingNumber = bdsAddress.BuildingNumber;
                address.Entrance = bdsAddress.Entrance;
                address.Floor = bdsAddress.Floor.HasValue ? bdsAddress.Floor.ToString() : null;
            }

            return address;
        }

        public void CheckForNonHandedAndNonPaidSlip(ApplicationFormInitializationRequest request, TView app)
        {
            //Прави се проверка в АИС АНД за неплатени задължения по връчени фишове/НП/АУАН и когато резултата от тази проверка е положителен, ПЕАУ ще извежда уведомително съобщение, без да се стопира заявяването на услугите.
            app.AuthorHasNonPaidSlip = request.HasNonPaidSlip;
        }

        #endregion
    }
}