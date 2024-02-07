using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ReportForChangingOwnershipNewOwnersDataValidator : EAUValidator<ReportForChangingOwnershipNewOwnersData>
    {
        public ReportForChangingOwnershipNewOwnersDataValidator()
        {
            EAURuleFor(m => m.NewOwners).RequiredSection();
            EAURuleForEach(m => m.NewOwners).EAUInjectValidator();
        }
    }
}
