using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Models
{
    public class ControlCouponDataItemVM
    {
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }

        public Ammunition Ammunition { get; set; }
        public Pyrotechnics Pyrotechnics { get; set; }
        public Explosives Explosives { get; set; }
        public Firearms Firearms { get; set; }
    }
}
