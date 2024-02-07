using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class EntityDataValidator : EAUValidator<EntityData>
    {
        public EntityDataValidator()
        {
            RuleSet("Entity", () =>
            {
                EAURuleFor(m => m.Name).MinLengthValidatior(1);
                EAURuleFor(m => m.NameTrans).MinLengthValidatior(1);
                EAURuleFor(m => m.FullName).MinLengthValidatior(1);
                EAURuleFor(m => m.Identifier).RequiredField().UICBulstatValidation();
                EAURuleFor(m => m.EntityManagmentAddress).EAUInjectValidator();
            });

            RuleSet("Farmer", () =>
            {
                EAURuleFor(m => m.Identifier).RequiredField().UICBulstatValidation().WithEAUErrorCode("GL_INVALID_BULSTAT_E"); ;
            });

            RuleSet("EntityOrFarmer", () =>
            {
                EAURuleFor(m => m.Identifier).RequiredField().UICBulstatValidation().WithEAUErrorCode("GL_INVALID_IDENT_E"); ;
            });
        }
    }
}