using CNSys;
using EAU.Documents;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models;
using EAU.KAT.Documents.Models.Forms;
using EAU.Nomenclatures;
using EAU.Users;
using EAU.Utilities;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.KAT.Documents
{
    internal class ApplicationForChangeRegistrationOfVehiclesService :
        ApplicationFormServiceBase<ApplicationForChangeRegistrationOfVehicles, ApplicationForChangeRegistrationOfVehiclesVM>
    {
        public ApplicationForChangeRegistrationOfVehiclesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ApplicationForChangeRegistrationOfVehicles;

        protected override PrintPreviewData PrintPreviewData { get { return null; } }

        protected override List<ElectronicServiceAuthorQualityType> GetPossibleAuthorQualities(ApplicationFormInitializationRequest reques)
        {
            return new List<ElectronicServiceAuthorQualityType>() { ElectronicServiceAuthorQualityType.Official };
        }

        protected override List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType> GetPossibleRecipientTypes(ApplicationFormInitializationRequest reques)
        {
            return new List<PersonAndEntityBasicDataVM.PersonAndEntityChoiceType>() { PersonAndEntityBasicDataVM.PersonAndEntityChoiceType.Person };
        }

        protected override List<IdentityDocumentType> GetPossibleRecipientIdentityDocumentTypes(ApplicationFormInitializationRequest request)
        {
            var service = GetService<IServices>().Search().Single(s => s.ServiceID == request.ServiceID);

            if (service.AdditionalConfiguration.ContainsKey("identityDocumentType"))
                return service.AdditionalConfiguration["identityDocumentType"].Split(',').Select(t => (IdentityDocumentType)Convert.ToInt32(t)).ToList();

            return new List<IdentityDocumentType>() { IdentityDocumentType.PersonalCard };
        }

        protected override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            request.AdditionalData["labelForOfficial"] = "DOC_GL_ElectronicServiceAuthorQualityType_OfficialNotarius_L";
            request.AdditionalData["hideRecipient"] = "true";

            return base.InitializeDocumentFormInternalAsync(request, cancellationToken);
        }

        protected async override Task<OperationResult> InitializeApplicationFormInternalAsync(
            ApplicationFormInitializationRequest request,
            CancellationToken cancellationToken)
        {
            var result = await base.InitializeApplicationFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
                return result;

            #region NotaryValidation

            var notaryInfo = await GetService<IUserNotaryService>().GetUserNotaryInfoAsync(cancellationToken);

            var app = (ApplicationForChangeRegistrationOfVehiclesVM)request.Form;

            if (notaryInfo.Errors != null || !notaryInfo.HasNotaryRights)
            {
                var localizer = GetService<IStringLocalizer>();
                var errors = new ErrorCollection { new TextError("GL_NOT_NOTARY_E", localizer["GL_NOT_NOTARY_E"].Value) };

                return new OperationResult(new ErrorCollection(errors));
            }
            else if (app.Circumstances == null)
            {
                app.Circumstances = new ApplicationForChangeRegistrationOfVehiclesDataVM()
                {
                    NotaryIdentityNumber = notaryInfo.NotaryNumber,
                    VehicleOwnershipChangeType = Domain.Models.VehicleOwnershipChangeType.ChangeOwnership,
                    ChangeRegistrationWithPersonOrEntity = new VehicleRegistrationChangeVM()
                    {
                        CurrentOwners = new List<VehicleOwnerDataVM>()
                        {
                            new VehicleOwnerDataVM()
                            {
                                PersonEntityData = new PersonEntityDataVM()
                                {
                                    SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Person,
                                    Person = new PersonDataVM()
                                    {
                                        Identifier = new PersonIdentifierVM()
                                        {
                                            ItemElementName = PersonIdentifierChoiceType.EGN
                                        }
                                    }
                                }
                            }
                        },

                        NewOwners = new List<VehicleBuyerDataVM>()
                        {
                            new VehicleBuyerDataVM()
                            {
                                PersonEntityData = new PersonEntityDataVM()
                                {
                                    SelectedPersonEntityFarmerChoiceType = PersonEntityFarmerChoiceType.Person,
                                    Person = new PersonDataVM()
                                    {
                                        Identifier = new PersonIdentifierVM()
                                        {
                                            ItemElementName = PersonIdentifierChoiceType.EGN
                                        }
                                    }
                                }
                            }
                        },

                        VehicleRegistrationData = new List<VehicleRegistrationDataVM>()
                        {
                            new VehicleRegistrationDataVM()
                            {
                                RegistrationCertificateType = Domain.Models.RegistrationCertificateTypeNomenclature.RegistrationDocument                                
                            }
                        }
                    }
                };
            }
            else
            {
                app.Circumstances.NotaryIdentityNumber = notaryInfo.NotaryNumber;
            }

            #endregion

            if (request.AdditionalData == null)
                request.AdditionalData = new AdditionalData();

            request.AdditionalData["skipValidateRecipients"] = "true";

            return result;
        }

        protected override Task<IErrorCollection> ValidateApplicationFormInternalAsync(ApplicationFormValidationRequest request, CancellationToken cancellationToken)
        {
            return base.ValidateApplicationFormInternalAsync(request, cancellationToken, true, true);
        }

        public override string SignatureXpath
        {
            get
            {
                return "afcrov:ApplicationForChangeRegistrationOfVehicles/afcrov:ElectronicAdministrativeServiceFooter/xmldsig:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"afcrov", "http://ereg.egov.bg/segment/R-3327"},
                    {"xmldsig", "http://ereg.egov.bg/segment/0009-000153"}
                };
            }
        }
    }
}