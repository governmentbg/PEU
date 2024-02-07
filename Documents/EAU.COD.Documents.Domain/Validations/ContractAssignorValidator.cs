using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.COD.Documents.Domain.Validations
{
    public class ContractAssignorValidator : EAUValidator<ContractAssignor>
    {
        public ContractAssignorValidator()
        {
            RuleSet("New", () =>
            {
                EAURuleFor(m => m.AssignorPersonEntityType).RequiredField();
                EAURuleFor(m => m.EntityAssignorData).RequiredSection().When(m => m.AssignorPersonEntityType == AssignorPersonEntityType.Entity).InjectValidator();
                EAURuleFor(m => m.PersonAssignorData).RequiredSection().When(m => m.AssignorPersonEntityType == AssignorPersonEntityType.Person).InjectValidator("NameAndIdentAndCitizenship");
            });
            RuleSet("Termination", () =>
            {
                EAURuleFor(m => m.AssignorPersonEntityType).RequiredField();
                EAURuleFor(m => m.EntityAssignorData).RequiredSection().When(m => m.AssignorPersonEntityType == AssignorPersonEntityType.Entity).InjectValidator();
                EAURuleFor(m => m.PersonAssignorData).RequiredSection().When(m => m.AssignorPersonEntityType == AssignorPersonEntityType.Person).InjectValidator("NameAndIdent");
            });
        }
    }
}