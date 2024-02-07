using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class NotificationForTakingOrRemovingFromSecurityDataValidator : EAUValidator<NotificationForTakingOrRemovingFromSecurityData>
    {
        public NotificationForTakingOrRemovingFromSecurityDataValidator()
        {
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.NotificationType).RequiredField();

            When(m => m.NotificationType == Models.NotificationType.NewSecurityContr235789 ||
             m.NotificationType == Models.NotificationType.NewSecurityContr4, () =>
             {
                 EAURuleFor(m => m.SecurityContractData).RequiredFieldFromSection().EAUInjectValidator();
             });

            When(m => m.NotificationType == Models.NotificationType.NewSecurityContr235789, () =>
             {
                 EAURuleFor(m => m.ContractAssignor).RequiredFieldFromSection();
                 EAURuleFor(m => m.ContractAssignor).InjectValidator("New");
             });
            When(m => m.NotificationType == Models.NotificationType.TerminationSecurityContr235789, () =>
             {
                 EAURuleFor(m => m.ContractAssignor).RequiredFieldFromSection();
                 EAURuleFor(m => m.ContractAssignor).InjectValidator("Termination");
             });
        }
    }
}