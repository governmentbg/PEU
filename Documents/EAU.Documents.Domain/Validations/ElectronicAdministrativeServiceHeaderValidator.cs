using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ElectronicAdministrativeServiceHeaderValidator : EAUValidator<ElectronicAdministrativeServiceHeader>
    {
        public ElectronicAdministrativeServiceHeaderValidator()
        {
            EAURuleFor(m => m.SUNAUServiceURI)
                .MinLengthValidatior(1)
                .RequiredField();
            EAURuleFor(m => m.DocumentTypeURI)
                .RequiredFieldFromSection()
                .EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName)
                .MinLengthValidatior(1)
                .RequiredFieldFromSection();
            EAURuleFor(m => m.ElectronicServiceProviderBasicData)
                .RequiredFieldFromSection()
                .EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator
            EAURuleFor(m => m.ElectronicServiceApplicant)
                .RequiredFieldFromSection()
                .EAUInjectValidator(); //ElectronicServiceApplicantValidator
            EAURuleFor(m => m.ElectronicServiceApplicantContactData).EAUInjectValidator(); //ElectronicServiceApplicantContactDataValidator
            EAURuleFor(m => m.SUNAUServiceName)
                .MinLengthValidatior(1)
                .RequiredFieldFromSection();
            EAURuleFor(m => m.DocumentURI)
                .RequiredFieldFromSection()
                .EAUInjectValidator() //DocumentURIValidator
                .When(m => m.ApplicationType != ApplicationType.AppForFirstReg);
            EAURuleFor(m => m.SendApplicationWithReceiptAcknowledgedMessage)
                .Equal(false)
                .WithEAUErrorCode(ErrorMessagesConstants.FieldCanContainsOnly, "false"); //По анализ тази функционалност не трябва да се ползва
        }
    }
}
