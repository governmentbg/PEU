using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    public class BudgetPaymentResponse : Message
    {
        public BudgetPaymentResponse()
            : base()
        {
        }

        [MessageItem(Name = "IDN")]
        public string IDNumer { get; set; }

        [MessageItem(Name = "ERR")]
        public string ErrorMessage { get; set; }
    }
}
