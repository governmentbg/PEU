using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class ApplicationByFormAnnex10DataValidator : EAUValidator<ApplicationByFormAnnex10Data>
    {
        public ApplicationByFormAnnex10DataValidator()
        {
            EAURuleFor(m => m.PersonalInformation)
                .RequiredField()
                .EAUInjectValidator();
            EAURuleFor(m => m.IssuingDocument)
                .RequiredField()
                .MinLengthValidatior(1);
            EAURuleFor(m => m.PersonGrantedFromIssuingDocument).EAUInjectValidator("NamesAndIdent");
          
            //SpecificDataForIssuingDocumentsForKOS валидацията за това пропърти беше закоментирана в автогенерирания файл.
        }
    }
}