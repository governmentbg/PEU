using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Services.Nomenclatures.Models
{
    public class UnitInfo
    {
        public int? UnitID { get; set; }

        public string Name { get; set; }
        
        public int? ParentUnitID { get; set; }
        
        public bool? HasChildUnits { get; set; }
    }
}
