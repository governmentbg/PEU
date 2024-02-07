using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class ReceiptAcknowledgedMessageRegisteredByOfficerVM
    {
        public AISUserNamesVM PersonNames
        {
            get;
            set;
        }

        public string AISUserIdentifier
        {
            get;
            set;
        }
    }

    public class AISUserNamesVM
    {
        public PersonNames ItemPersonNames
        {
            get;
            set;
        }

        public ForeignCitizenNames ItemForeignCitizenNames
        {
            get;
            set;
        }
    }
}
