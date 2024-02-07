using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;
using FluentValidation.Results;
using EAU.Documents.Domain.Models;
using System.Linq;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForInitialVehicleRegistrationValidator : EAUValidator<ApplicationForInitialVehicleRegistration>
    {
        public ApplicationForInitialVehicleRegistrationValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForInitialVehicleRegistrationData).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForInitialVehicleRegistration> context)
        {
            ValidationResult result = base.Validate(context);
            ApplicationForInitialVehicleRegistration instanceToValidate = context.InstanceToValidate;

            var author = instanceToValidate.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Authors[0];
            var owners = instanceToValidate.ApplicationForInitialVehicleRegistrationData?.OwnersCollection?.InitialVehicleRegistrationOwnerData;

            if (owners != null
                && owners.Count > 0
                && author.AuthorQuality is ElectronicServiceAuthorQualityType authorQualityType)
            {
                PlaceHolder[] parameters = new PlaceHolder[]
                        {
                            new PlaceHolder()
                            {
                                Name = "Field",
                                ResourceCode = FluentValidationExtensions.GetObjectResourseCode(instanceToValidate.ApplicationForInitialVehicleRegistrationData.OwnersCollection.GetType())
                            }
                        };

                if (authorQualityType == ElectronicServiceAuthorQualityType.Personal
                    && !owners.Any(o => o.Item is PersonIdentifier identifier
                        && identifier.ItemElementName == author.ItemPersonBasicData.Identifier.ItemElementName
                        && string.Compare(identifier.Item, author.ItemPersonBasicData.Identifier.Item, true) == 0))
                {
                    //Когато заявителят е в лично качество, полето "Данни за собствениците на ППС" трябва да има поне един собственик, който е заявителя.
                    AddValidationFailure(result, "DOC_KAT_InitialVehicleRegistrationOwners_Author_Person_E", parameters);
                }

                if (authorQualityType != ElectronicServiceAuthorQualityType.Personal)
                {
                    if (owners.Any(o => o.Item is PersonIdentifier identifier
                                    && identifier.ItemElementName == author.ItemPersonBasicData.Identifier.ItemElementName
                                    && string.Compare(identifier.Item, author.ItemPersonBasicData.Identifier.Item, true) == 0))
                    {
                        //Полето "Данни за собствениците на ППС" не може да съдържа заявителят, когато за качеството на заявителя не е посочено "лично качество".
                        AddValidationFailure(result, "DOC_KAT_ApplicationForInitialVehicleRegistrationDataOwnersCollection_AuthorNotAllowed_L", parameters);
                    }

                    var recepients = instanceToValidate.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients;

                    if (recepients != null
                        && recepients.Count > 0
                        && recepients[0] != null)
                    {
                        if (authorQualityType == ElectronicServiceAuthorQualityType.LegalRepresentative
                            && recepients[0].ItemEntityBasicData != null
                            && !owners.Any(o => o.Item is string firm && string.Compare(firm, recepients[0].ItemEntityBasicData.Identifier, true) == 0))
                        {
                            //Когато заявителят е законен представител на юридическото лице, полето "Данни за собствениците на ППС" трябва да съдържа поне един собственик - юридическото лице.
                            AddValidationFailure(result, "DOC_KAT_InitialVehicleRegistrationOwners_Author_LegalRepresentative_E", parameters);
                        }

                        if (authorQualityType == ElectronicServiceAuthorQualityType.Representative)
                        {
                            if ((recepients[0].ItemEntityBasicData != null
                                 && !owners.Any(o => o.Item is string firm 
                                                    && string.Compare(firm, recepients[0].ItemEntityBasicData.Identifier, true) == 0))
                                ||
                                (recepients[0].ItemPersonBasicData != null
                                    && !owners.Any(o => o.Item is PersonIdentifier identifier
                                                        && identifier.ItemElementName == recepients[0].ItemPersonBasicData.Identifier.ItemElementName
                                                        && string.Compare(identifier.Item, recepients[0].ItemPersonBasicData.Identifier.Item, true) == 0)))
                            {
                                //Като заявител на услугата в качеството на пълномощник на друго физическо/юридическо лице, в полето "<Field>" трябва да присъства упълномощителя (посочен като получател на услугата).
                                AddValidationFailure(result, "DOC_KAT_InitialVehicleRegistrationOwners_Author_Attorney_E", parameters);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}