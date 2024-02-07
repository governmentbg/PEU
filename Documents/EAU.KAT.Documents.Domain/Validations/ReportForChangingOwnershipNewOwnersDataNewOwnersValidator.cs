using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ReportForChangingOwnershipNewOwnersDataNewOwnersValidator : EAUValidator<ReportForChangingOwnershipNewOwnersDataNewOwners>
    {
        public ReportForChangingOwnershipNewOwnersDataNewOwnersValidator()
        {
            EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
            {
                v.EAUAdd(new EntityDataValidator(), ruleSets: "Entity");
                v.EAUAdd(new PersonDataValidator(), "PersonIdentifier");
            });
        }
    }
}
