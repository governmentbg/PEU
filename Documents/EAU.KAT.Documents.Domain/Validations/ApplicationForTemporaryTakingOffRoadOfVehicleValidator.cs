using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForTemporaryTakingOffRoadOfVehicleValidator : EAUValidator<ApplicationForTemporaryTakingOffRoadOfVehicle>
    {
        public ApplicationForTemporaryTakingOffRoadOfVehicleValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
            EAURuleFor(m => m.VehicleDataRequest).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForTemporaryTakingOffRoadOfVehicle> context)
        {
            ValidationResult result = base.Validate(context);
            ApplicationForTemporaryTakingOffRoadOfVehicle instanceToValidate = context.InstanceToValidate;

            var author = instanceToValidate.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Authors[0];
            var owners = instanceToValidate.VehicleDataRequest?.OwnersCollection?.Owners;

            if (owners != null
                && owners.Count > 0
                && author.AuthorQuality is ElectronicServiceAuthorQualityType authorQualityType)
            {
                PlaceHolder[] parameters = new PlaceHolder[]
                    {
                        new PlaceHolder()
                        {
                            Name = "Field",
                            ResourceCode = FluentValidationExtensions.GetObjectResourseCode(instanceToValidate.VehicleDataRequest.OwnersCollection.GetType())
                        }
                    };

                if (authorQualityType == ElectronicServiceAuthorQualityType.Personal
                    && !owners.Any(o => o.Item is PersonIdentifier identifier
                        && string.Compare(identifier.Item, author.ItemPersonBasicData.Identifier.Item, true) == 0))
                {
                    //Когато заявителят е в лично качество, полето "Собственици по документ за регистрация на ППС" трябва да има поне един собственик, който е заявителя.
                    AddValidationFailure(result, "DOC_KAT_00002_E", parameters);
                }

                if (authorQualityType == ElectronicServiceAuthorQualityType.LegalRepresentative
                    && !owners.Any(o => o.Item is string))
                {
                    //Когато заявителят е законен представител на юридическото лице, полето "Собственици по документ за регистрация на ППС" трябва да съдържа поне един собственик - юридическото лице.
                    AddValidationFailure(result, "DOC_KAT_00003_E", parameters);
                }

                if ((authorQualityType == ElectronicServiceAuthorQualityType.Representative
                    || authorQualityType == ElectronicServiceAuthorQualityType.LegalRepresentative)
                    && owners.Any(o => o.Item is PersonIdentifier identifier
                        && identifier.ItemElementName == author.ItemPersonBasicData.Identifier.ItemElementName
                        && string.Compare(identifier.Item, author.ItemPersonBasicData.Identifier.Item, true) == 0))
                {
                    //Когато не подавате заявлението в лично качество, в полето "<Field>" не трябва да присъства заявителя. 
                    //В случай че сте един от собствениците на ППС, моля, изберете да подадете заявлението по услугата в "лично качество“.
                    AddValidationFailure(result, "DOC_KAT_00004_E", parameters);
                }
            }

            return result;
        }
    }
}