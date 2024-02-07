using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpirationTimeAttribute : Attribute
    {
        public ExpirationTimeAttribute()
        { }
    }
}
