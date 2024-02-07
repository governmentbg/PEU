using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class DataForPrintSRMPSDataHolderDataValidator : EAUValidator<DataForPrintSRMPSDataHolderData>
    {
        public DataForPrintSRMPSDataHolderDataValidator()
        {
            EAURuleFor(m => m.Item).RequiredFieldFromSection().SetInheritanceValidator(v => 
            {
                v.EAUAdd(new EntityDataValidator(), ruleSets: "EntityOrFarmer");
                v.EAUAdd(new PersonDataValidator(), "PersonIdentifier");
            });
        }
    }
}