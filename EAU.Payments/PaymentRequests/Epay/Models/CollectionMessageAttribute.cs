using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CollectionMessageAttribute : Attribute
    {
        public string CollectionName
        {
            get;
            private set;
        }

        public CollectionMessageAttribute(string name)
        {
            CollectionName = name;
        }
    }
}
