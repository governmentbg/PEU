using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Validation
{
    /// <summary>
    /// Used for specifying fields content on outgoing messages.
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class RegularExpressionAttribute : Attribute
    {
        public string Pattern
        {
            get;
            private set;
        }

        public RegularExpressionAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
