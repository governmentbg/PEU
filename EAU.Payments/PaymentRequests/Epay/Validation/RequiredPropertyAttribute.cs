using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Validation
{
    /// <summary>
    /// Used for specifying required fields on outgoing messages.
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredPropertyAttribute : Attribute
    {
        public bool Required
        {
            get;
            private set;
        }

        public RequiredPropertyAttribute(bool required)
        {
            Required = required;
        }
    }
}
