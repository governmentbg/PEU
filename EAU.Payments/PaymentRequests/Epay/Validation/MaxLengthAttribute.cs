using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        public int Length
        {
            get;
            private set;
        }

        public MaxLengthAttribute(int length)
        {
            Length = length;
        }
    }
}
