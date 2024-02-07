using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.COD.Documents.Domain.Validations
{
    public class EntityAssignorDataValidator : EAUValidator<EntityAssignorData>
    {
        public EntityAssignorDataValidator()
        {
            EAURuleFor(m => m.FullName).RequiredField();
            EAURuleFor(m => m.FullName).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
            EAURuleFor(m => m.Identifier).RequiredField();
            EAURuleFor(m => m.Identifier).UICBulstatValidation();
        }
    }
}
