using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using System;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class IssuedBulgarianIdentityDocumentInfoValidator : EAUValidator<IssuedBulgarianIdentityDocumentInfo>
    {
        public IssuedBulgarianIdentityDocumentInfoValidator()
        {
            EAURuleFor(m => m.IssuingYear).RequiredField().When(m => m.DocType.HasValue && m.IssuingYear == null);
            EAURuleFor(m => m.IssuingYear).Must(val =>
            {
                if (val.HasValue)
                {
                    return (val.Value >= 2000 && val.Value <= DateTime.Now.Year);
                }
                else
                {
                    return true;
                }
            }).WithEAUErrorCode("DOC_BDS_DocumentToBeIssuedFor_YearofissueConnectedWithIssuedBulgarianIdentityDocuments_E");
            EAURuleFor(m => m.DocType).RequiredField().When(m => m.DocType == null && m.IssuingYear.HasValue);
        }
    }
}
