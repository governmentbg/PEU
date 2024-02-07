using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Services.Nomenclatures.Models
{
    public class ServiceIrregularity
    {
        public int? ServiceIrregularityID { get; set; }

        public int? ServiceID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
