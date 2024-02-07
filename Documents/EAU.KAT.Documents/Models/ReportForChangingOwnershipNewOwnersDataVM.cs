using EAU.KAT.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.KAT.Documents.Models
{
    public class ReportForChangingOwnershipNewOwnersDataVM
    {
        public List<ReportForChangingOwnershipNewOwnersDataNewOwnersVM> NewOwners
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
