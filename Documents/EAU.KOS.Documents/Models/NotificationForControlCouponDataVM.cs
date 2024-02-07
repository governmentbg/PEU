using EAU.KOS.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KOS.Documents.Models
{
    public class NotificationForControlCouponDataVM
    {
        public LicenseInfo LicenseInfo { get; set; }

        public List<ControlCouponDataItemVM> ControlCouponData { get; set; }
    }
}
