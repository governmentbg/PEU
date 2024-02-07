using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.COD.Documents.Domain.Validations
{
    public class SecurityObjectsDataValidator : EAUValidator<SecurityObjectsData>
    {        
        public SecurityObjectsDataValidator()
        {
            RuleSet("New", () => {               
                EAURuleForEach(m => m.SecurityObjects).EAUInjectValidator("New");
            });
            RuleSet("Termination", () => {              
                EAURuleForEach(m => m.SecurityObjects).EAUInjectValidator("Termination");
            });
        }
    }
}