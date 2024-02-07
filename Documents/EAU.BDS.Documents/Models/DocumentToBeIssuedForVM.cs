using EAU.Documents.Models;

namespace EAU.BDS.Documents.Models
{
    public enum IssueDocumentFor
    {
        IssuedBulgarianIdentityDocumentsInPeriod = 1,

        OtherInformationConnectedWithIssuedBulgarianIdentityDocuments = 2
    }

    public class DocumentToBeIssuedForVM
    {
        public IssueDocumentFor ChooseIssuingDocument { get; set; }

        public IssuedBulgarianIdentityDocumentsInPeriodVM IssuedBulgarianIdentityDocumentsInPeriod { get; set; }
      
        public OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM OtherInformationConnectedWithIssuedBulgarianIdentityDocuments { get; set; }
  
        public DocumentMustServeToVM DocumentMustServeTo { get; set; }
    }
}