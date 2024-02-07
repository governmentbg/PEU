using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CollectionAttribute : Attribute
    {
        public Type ItemType
        {
            get;
            set;
        }
    }
}
