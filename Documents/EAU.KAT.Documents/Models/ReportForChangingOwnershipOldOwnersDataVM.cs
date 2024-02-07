using EAU.KAT.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.KAT.Documents.Models
{
    public class ReportForChangingOwnershipOldOwnersDataVM
    {
        public List<ReportForChangingOwnershipOldOwnersDataOldOwnersVM> OldOwners
        {
            get;
            set;
        }
        public List<Status> Status
        {
            get;
            set;
        }
    }
}
