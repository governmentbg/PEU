using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models.Forms;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class ApplicationByFormAnnex10Validator : EAUValidator<ApplicationByFormAnnex10>
    {
        public ApplicationByFormAnnex10Validator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationByFormAnnex10Data).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
        public override ValidationResult Validate(ValidationContext<ApplicationByFormAnnex10> context)
        {

            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            //TODO: За да се изисква секцията PersonGrantedFromIssuingDocument да бъде попълнена, трябва и самото заявление да е маркирано, че може да се попълва от трети лица. 
            //Не е достатъчно само получателя да е юридическо лице.
            
            //if (instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups != null
            //    && instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients.Count > 0
            //    )
            //{
            //    if (instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].Item != null
            //        && instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].Item is EntityBasicData)
            //    {
            //        if (instance.ApplicationByFormAnnex10Data.PersonGrantedFromIssuingDocument == null)
            //            AddValidationFailure(validationRes, "DOC_KOS_ApplicationByFormAnnex10Data_personGrantedFromIssuingDocument_E");
            //    }
            //}            

            return validationRes;
        }
    }
}