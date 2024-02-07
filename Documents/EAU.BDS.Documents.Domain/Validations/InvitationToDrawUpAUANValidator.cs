using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class InvitationToDrawUpAUANValidator : EAUValidator<InvitationToDrawUpAUAN>
    {
        public InvitationToDrawUpAUANValidator()
        {
            EAURuleFor(m => m.DocumentURI)
                .EAUInjectValidator(); //DocumentURIValidator

            EAURuleFor(m => m.DocumentTypeName).RequiredField();

            EAURuleFor(m => m.ElectronicServiceProviderBasicData)
               .RequiredField()
               .EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator

            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
        }
    }
}