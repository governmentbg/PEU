using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ReportForChangingOwnershipOldOwnersDataValidator : EAUValidator<ReportForChangingOwnershipOldOwnersData>
    {
        public ReportForChangingOwnershipOldOwnersDataValidator()
        {
            EAURuleFor(m => m.OldOwners).RequiredSection();
            EAURuleForEach(m => m.OldOwners).EAUInjectValidator();
        }
    }
}
