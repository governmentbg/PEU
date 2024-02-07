using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models.Forms
{
    public interface IApplicationForm : IDocumentForm
    {
        ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader { get; set; }

        List<AttachedDocument> AttachedDocuments { get; set; }

        ServiceTermType? ServiceTermType { get; set; }

        List<Declaration> Declarations { get; set; }

        ServiceApplicantReceiptData ServiceApplicantReceiptData { get; set; }

        ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter { get; set; }
    }
}
