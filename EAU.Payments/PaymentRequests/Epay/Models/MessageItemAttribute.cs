using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    /// <summary>
    /// Used to specify name of proparty in message
    /// </summary>
    /// 
    [AttributeUsage(AttributeTargets.Property)]
    public class MessageItemAttribute : Attribute
    {
        /// <summary>
        /// Name of the MessageItem
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Property name of a proprty, that returns the Name of the MessageItem
        /// </summary>
        public string DynamicNameReference
        {
            get;
            set;
        }
    }
}
