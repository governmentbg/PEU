using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.COD.Documents.Domain.Validations
{
    public class NotificationForConcludingOrTerminatingEmploymentContractDataValidator : EAUValidator<NotificationForConcludingOrTerminatingEmploymentContractData>
    {
        public NotificationForConcludingOrTerminatingEmploymentContractDataValidator()
        {
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField();
            EAURuleFor(m => m.NotificationOfEmploymentContractType).RequiredField();

            When(m => m.NotificationOfEmploymentContractType == NotificationOfEmploymentContractType.Concluding, () =>
            {
                EAURuleFor(m => m.NewEmployeeRequests).RequiredField();
                EAURuleForEach(m => m.NewEmployeeRequests).EAUInjectValidator();
            });

            When(m => m.NotificationOfEmploymentContractType == NotificationOfEmploymentContractType.Terminating, () =>
            {
                EAURuleFor(m => m.RemoveEmployeeRequests).RequiredField();
                EAURuleForEach(m => m.RemoveEmployeeRequests).EAUInjectValidator();
            });
        }
    }

    public class NewEmployeeRequestValidator : EAUValidator<NewEmployeeRequest>
    {
        public NewEmployeeRequestValidator()
        {
            EAURuleFor(m => m.Employee).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.ContractNumber).RangeLengthValidatior(1, 150);
            EAURuleFor(m => m.ContractDate).RequiredField();
            EAURuleFor(m => m.ContractType).RequiredField();

            When(m => m.ContractType == ContractType.ForPeriod, () =>
            {
                EAURuleFor(m => m.ContractPeriodInMonths).RequiredField();
                EAURuleFor(m => m.ContractPeriodInMonths).MatchesValidatior("^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed);
            });
        }

        public override ValidationResult Validate(ValidationContext<NewEmployeeRequest> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (validationRes.IsValid)
            {
                DateTime toDayEndOfDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                if (instance.ContractDate > toDayEndOfDay)
                {
                    EAUValidator<NewEmployeeRequest>.PlaceHolder[] parameters = new EAUValidator<NewEmployeeRequest>.PlaceHolder[]
                    {
                        new EAUValidator<NewEmployeeRequest>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "ContractDate")),
                        new EAUValidator<NewEmployeeRequest>.PlaceHolder("Param1", toDayEndOfDay.ToShortDateString())
                    };

                    AddValidationFailureWithoutParamsTranslation(validationRes, ErrorMessagesConstants.FiledLessThenOrEqual, parameters);
                }
            }

            return validationRes;
        }
    }

    public class RemoveEmployeeRequestValidator : EAUValidator<RemoveEmployeeRequest>
    {
        public RemoveEmployeeRequestValidator()
        {
            EAURuleFor(m => m.Employee).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.ContractTerminationNumber).RangeLengthValidatior(1, 150);
            EAURuleFor(m => m.ContractTerminationDate).RequiredField();
            EAURuleFor(m => m.ContractTerminationEffectiveDate).RequiredField();
        }

        public override ValidationResult Validate(ValidationContext<RemoveEmployeeRequest> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (validationRes.IsValid)
            {
                DateTime toDayEndOfDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                if (instance.ContractTerminationDate > toDayEndOfDay)
                {
                    EAUValidator<RemoveEmployeeRequest>.PlaceHolder[] parameters = new EAUValidator<RemoveEmployeeRequest>.PlaceHolder[]
                    {
                        new EAUValidator<RemoveEmployeeRequest>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "ContractTerminationDate")),
                        new EAUValidator<RemoveEmployeeRequest>.PlaceHolder("Param1", toDayEndOfDay.ToShortDateString())
                    };

                    AddValidationFailureWithoutParamsTranslation(validationRes, ErrorMessagesConstants.FiledLessThenOrEqual, parameters);
                }

                if (instance.ContractTerminationEffectiveDate < instance.ContractTerminationDate)
                {
                    EAUValidator<RemoveEmployeeRequest>.PlaceHolder[] parameters = new EAUValidator<RemoveEmployeeRequest>.PlaceHolder[]
                    {
                        new EAUValidator<RemoveEmployeeRequest>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "ContractTerminationEffectiveDate")),
                        new EAUValidator<RemoveEmployeeRequest>.PlaceHolder("Param1", instance.ContractTerminationDate.Value.ToShortDateString())
                    };

                    AddValidationFailureWithoutParamsTranslation(validationRes, ErrorMessagesConstants.FiledGreatherThanOrEqual, parameters);
                }
            }

            return validationRes;
        }
    }

    public class EmployeeValidator : EAUValidator<Employee>
    {
        public EmployeeValidator()
        {
            EAURuleFor(m => m.FullName).RequiredField().RangeLengthValidatior(1, 150);
            EAURuleFor(m => m.EmployeeIdentifierType).RequiredField();
            EAURuleFor(m => m.Identifier).RequiredField().EgnValidation().When(m => m.EmployeeIdentifierType == EmployeeIdentifierType.EGN);
            EAURuleFor(m => m.Identifier).RequiredField().LnchValidation().When(m => m.EmployeeIdentifierType == EmployeeIdentifierType.LN);
            EAURuleFor(m => m.Citizenship).RequiredField();
        }
    }
}