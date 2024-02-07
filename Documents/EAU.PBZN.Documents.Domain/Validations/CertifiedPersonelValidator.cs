using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class CertifiedPersonelValidator : EAUValidator<CertifiedPersonel>
    {
        public CertifiedPersonelValidator()
        {
            EAURuleFor(m => m.PersonFirstName).RequiredField()
                .CyrillicNameValidatior()
                .RangeLengthValidatior(1, 30);
            EAURuleFor(m => m.PersonLastName).RequiredField()
                .CyrillicNameValidatior()
                .RangeLengthValidatior(1, 50);
            EAURuleFor(m => m.CertificateNumber).RequiredField();
        }
    }
}
