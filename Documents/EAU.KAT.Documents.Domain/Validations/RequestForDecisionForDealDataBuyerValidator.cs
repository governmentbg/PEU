using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class RequestForDecisionForDealDataBuyerValidator : EAUValidator<RequestForDecisionForDealDataBuyer>
    {
        public RequestForDecisionForDealDataBuyerValidator()
        {
            EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v => {
                v.EAUAdd(new PersonDataValidator(), ruleSets: "PersonIdentifierDocument");
                v.EAUAdd(new EntityDataValidator(), ruleSets: "Entity");
            });
            EAURuleFor(m => m.EmailAddress).EmailValidation();
        }        
    }
}