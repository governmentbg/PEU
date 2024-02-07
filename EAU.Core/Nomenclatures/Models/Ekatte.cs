using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{ 
    public class Ekatte
    {
        [DapperColumn("ekatte_id")]
        public int EkatteID
        {
            get;
            set;
        }

        [DapperColumn("name")]
        public string Name
        {
            get;
            set;
        }

        [DapperColumn("ekatte_type")]
        public EkatteTypes? EkatteTypeID
        {
            get;
            set;
        }

        [DapperColumn("code")]
        public string Code
        {
            get;
            set;
        }

        [DapperColumn("parent_id")]
        public int? ParentID
        {
            get;
            set;
        }
    }
}
