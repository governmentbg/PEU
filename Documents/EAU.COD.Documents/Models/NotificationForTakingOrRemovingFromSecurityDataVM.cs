using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Models;

namespace EAU.COD.Documents.Models.Forms
{
    public class NotificationForTakingOrRemovingFromSecurityDataVM
	{
        public PoliceDepartment IssuingPoliceDepartment { get; set; }
       
        public NotificationType? NotificationType { get; set; }

        public SecurityContractData SecurityContractData { get; set; }

        public ContractAssignor ContractAssignor { get; set; }
    }
}