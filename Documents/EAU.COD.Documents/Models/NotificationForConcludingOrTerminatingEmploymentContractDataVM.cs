using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.COD.Documents.Models.Forms
{
    public class NotificationForConcludingOrTerminatingEmploymentContractDataVM
	{
		public PoliceDepartment IssuingPoliceDepartment { get; set; }
		
		public NotificationOfEmploymentContractType? NotificationOfEmploymentContractType { get; set; }

		public List<NewEmployeeRequest> NewEmployeeRequests { get; set; }

		public List<RemoveEmployeeRequest> RemoveEmployeeRequests { get; set; }
	}
}