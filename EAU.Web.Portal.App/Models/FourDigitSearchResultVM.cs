using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WAIS.Integration.MOI.KAT.SPRKRTCO.Models;

namespace EAU.Web.Portal.App.Models
{
    public class FourDigitSearchResultVM
    {
        public string ExceedResultLimiteWarnning { get; set; }

        public List<PlateStatusResult> Result { get; set; }
    }
}
