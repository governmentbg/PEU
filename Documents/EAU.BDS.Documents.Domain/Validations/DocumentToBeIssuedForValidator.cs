using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class DocumentToBeIssuedForValidator : EAUValidator<DocumentToBeIssuedFor>
    {
        public DocumentToBeIssuedForValidator()
        {
            EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
            {
                v.EAUAdd(new IssuedBulgarianIdentityDocumentsInPeriodValidator());
                v.EAUAdd(new OtherIndormationConnectedWithIssuedBulgarianIdentityDocumentsValidator());
            });

            When(m => m.DocumentMustServeTo == null, () =>
            {
                EAURuleFor(m => m.DocumentMustServeTo).RequiredSection();
            })
            .Otherwise(() =>
            {
                EAURuleFor(m => m.DocumentMustServeTo).EAUInjectValidator();
            });
        }
    }
}
