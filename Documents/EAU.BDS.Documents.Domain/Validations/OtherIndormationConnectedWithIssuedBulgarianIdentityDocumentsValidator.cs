using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class OtherIndormationConnectedWithIssuedBulgarianIdentityDocumentsValidator : EAUValidator<Models.OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments>
    {
        public OtherIndormationConnectedWithIssuedBulgarianIdentityDocumentsValidator()
        {
            EAURuleForEach(m => m.DocumentNumbers).MatchesValidatior("^[A-Z]{2}\\d{7}$|^\\d{9}$");
            EAURuleForEach(m => m.DocumentsInfos).EAUInjectValidator();
            EAURuleFor(m => m.IncludsDataInCertificate)
                .CollectionWithAtLeastOneElement()
                .When(m => (m.DocumentNumbers != null && m.DocumentNumbers.Any(el => !string.IsNullOrEmpty(el))) 
                            || 
                            (m.DocumentsInfos != null && m.DocumentsInfos.Any(di => di.IssuingYearSpecified && di.DocTypeSpecified)));
        }

        public override ValidationResult Validate(ValidationContext<OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (validationRes.IsValid)
            {
                if((instance.DocumentNumbers != null && !(instance.DocumentNumbers.Any(dn => !string.IsNullOrEmpty(dn)))
                    || (instance.DocumentsInfos != null 
                        && instance.DocumentsInfos.Any(di => ((di.IssuingYear.HasValue && !di.DocType.HasValue) || (!di.IssuingYear.HasValue && di.DocType.HasValue))))))
                {
                    //Задължително трябва да въведете номер или година на издаване и вид на Ваш български личен документ.
                    AddValidationFailure(validationRes, "DOC_BDS_DocumentToBeIssuedFor_NumberOrYearAndTypeofissueConnectedWithIssuedBulgarianIdentityDocuments_E");
                }

                if (instance.IncludsDataInCertificate != null
                    && instance.IncludsDataInCertificate.Any(el => el == DataContainsInCertificateNomenclature.PermanentAddress)
                    && (instance.DocumentNumbers == null || instance.DocumentNumbers.Count == 0 || instance.DocumentNumbers.All(el => string.IsNullOrEmpty(el)))
                    && instance.DocumentsInfos != null
                    && !instance.DocumentsInfos.Any(di => di.DocTypeSpecified && di.DocType == BulgarianIdentityDocumentTypes.IDCard))
                {
                    //Трябва да въведете поне един документ от тип лична карта.
                    AddValidationFailure(validationRes, "DOC_BDS_DocumentOfType_IDCard_E");
                }
            }

            return validationRes;
        }
    }
}
