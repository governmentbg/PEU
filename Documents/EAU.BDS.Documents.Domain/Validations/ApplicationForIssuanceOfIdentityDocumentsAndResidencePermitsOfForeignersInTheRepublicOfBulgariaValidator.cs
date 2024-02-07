using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaValidator : EAUValidator<ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria>
    {
        public ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator(); //ElectronicAdministrativeServiceHeaderValidator
            EAURuleFor(m => m.ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData)
                .EAUInjectValidator();// ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaDataValidator
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator(); //PoliceDepartmentValidator
            //EAURuleFor(m => m.IdentificationPhotoAndSignature).EAUInjectValidator(); //IdentificationPhotoAndSignatureValidator 
            //EAURuleFor(m => m.ElectronicAdministrativeServiceFooter).EAUInjectValidator();
            EAURuleFor(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();


            //TODO: В момента се проверява дали има поне един документ.
            //Правилната валидация е да се проверява дали има поне 
            //удостоверителен документ от ТЕЛК за трайно увреждане 50 и над 50%
            //EAURuleFor(m => m.HasDocumentForDisabilities).Must((obj, val) =>
            //{
            //    if (val.GetValueOrDefault())
            //        return obj.AttachedDocuments != null && obj.AttachedDocuments.Count > 0;
            //    else
            //        return true;
            //}).WithEAUErrorCode(ErrorMessagesConstants.RequireDocumentForDisabilities);
        }
    }
}