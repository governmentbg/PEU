using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    public class Grao
    {
        [DapperColumn("grao_id")]
        public int GraoID
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

        [DapperColumn("grao_type")]
        public GraoTypes? GraoTypeID
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
