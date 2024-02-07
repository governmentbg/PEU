using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DocumentKindBaseRequiredAttribute : Attribute
    {
        public int[] DocumentKinds { get; set; }
        public string DocumentKindProperty { get; set; }

        public DocumentKindBaseRequiredAttribute(string documentKindProperty, params int[] args)
        {
            DocumentKindProperty = documentKindProperty;
            DocumentKinds = args;
        }
    }
}
