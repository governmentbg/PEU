using EAU.COD.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;
using FluentValidation.Results;
using FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class NotificationForTakingOrRemovingFromSecurityValidator : EAUValidator<NotificationForTakingOrRemovingFromSecurity>
    {
        public NotificationForTakingOrRemovingFromSecurityValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.NotificationForTakingOrRemovingFromSecurityData).EAUInjectValidator();
            EAURuleFor(m => m.SecurityObjectsData).RequiredField();

            When(m => m.NotificationForTakingOrRemovingFromSecurityData.NotificationType == Models.NotificationType.NewSecurityContr235789 ||
             m.NotificationForTakingOrRemovingFromSecurityData.NotificationType == Models.NotificationType.NewSecurityContr4, () =>
            {
                EAURuleFor(m => m.SecurityObjectsData).EAUInjectValidator("New");
            })
             .Otherwise(() =>
             {
                 EAURuleFor(m => m.SecurityObjectsData).EAUInjectValidator("Termination");
             });

            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
        public override ValidationResult Validate(ValidationContext<NotificationForTakingOrRemovingFromSecurity> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (instance.NotificationForTakingOrRemovingFromSecurityData.ContractAssignor != null)
            {
                if (instance.SecurityObjectsData != null && instance.SecurityObjectsData.SecurityObjects != null)
                {
                    for (int i = 0; i < instance.SecurityObjectsData.SecurityObjects.Count; i++)
                    {
                        if (instance.SecurityObjectsData.SecurityObjects[i].PointOfPrivateSecurityServicesLaw == Models.PointOfPrivateSecurityServicesLaw.PersonalSecurityServicesForPersons)
                        {
                            if (instance.NotificationForTakingOrRemovingFromSecurityData.ContractAssignor.AssignorPersonEntityType == Models.AssignorPersonEntityType.Entity)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ContractAssignor_assignorPersonEntityType_MustBePerson_E");
                                break;
                            }
                        }
                        if (instance.SecurityObjectsData.SecurityObjects[i].PointOfPrivateSecurityServicesLaw == Models.PointOfPrivateSecurityServicesLaw.RealEstatSecurity)
                        {
                            if (instance.NotificationForTakingOrRemovingFromSecurityData.ContractAssignor.AssignorPersonEntityType == Models.AssignorPersonEntityType.Person)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ContractAssignor_assignorPersonEntityType_MustBeEntity_E");
                                break;
                            }
                        }
                    }
                }
            }

            if (instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData != null && instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate != null)
            {
                if (instance.SecurityObjectsData != null && instance.SecurityObjectsData.SecurityObjects != null)
                    for (int i = 0; i < instance.SecurityObjectsData.SecurityObjects.Count; i++)
                    {
                        var obj = instance.SecurityObjectsData.SecurityObjects[i];
                        if (obj.PersonalSecurity != null)
                        {
                            if (obj.PersonalSecurity.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.PersonalSecurity.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.AlarmAndSecurityActivity != null)
                        {
                            if (obj.AlarmAndSecurityActivity.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.AlarmAndSecurityActivity.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.ProtectionOfAgriculturalProperty != null)
                        {
                            if (obj.ProtectionOfAgriculturalProperty.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.ProtectionOfAgriculturalProperty.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.ProtectionPersonsProperty != null)
                        {
                            if (obj.ProtectionPersonsProperty.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.ProtectionPersonsProperty.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.SecurityOfEvents != null)
                        {
                            if (obj.SecurityOfEvents.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.SecurityOfEvents.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.SecurityOfSitesRealEstate != null)
                        {
                            if (obj.SecurityOfSitesRealEstate.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.SecurityOfSitesRealEstate.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.SecurityTransportingCargo != null)
                        {
                            if (obj.SecurityTransportingCargo.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.SecurityTransportingCargo.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                        if (obj.SelfProtectionPersonsProperty != null)
                        {
                            if (obj.SelfProtectionPersonsProperty.TerminationDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_TerminationDate_E");
                                break;
                            }
                            if (obj.SelfProtectionPersonsProperty.ActualDate < instance.NotificationForTakingOrRemovingFromSecurityData.SecurityContractData.ContractDate)
                            {
                                AddValidationFailure(validationRes, "DOC_COD_ActualDate_E");
                                break;
                            }
                        }
                    }
            }

            return validationRes;
        }
    }
}