using EAU.Payments.PaymentRequests.Epay.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    public class NotificationMessage : Message
    {
        private static readonly string _dateTimeFormatString = "yyyyMMddHHmmss";
        private static readonly char _separator = ':';

        public NotificationMessage()
            : base()
        {
            dateTimeFormatString = _dateTimeFormatString;
            separator = _separator;
        }
    }

    [CollectionMessage("Rows")]
    public class NotificationMessageRequest : NotificationMessage
    {
        public NotificationMessageRequest()
            : base()
        {
            _Rows = new List<NotificationMessaRequestRow>();
        }

        private List<NotificationMessaRequestRow> _Rows;
        /// <summary>
        /// Колекция от редове за всеки отделен Invoice в NotificationMessageRequest-а
        /// </summary>
        [Collection(ItemType = typeof(NotificationMessaRequestRow))]
        public List<NotificationMessaRequestRow> Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }
    }

    public class NotificationMessaRequestRow
    {
        [MessageItem(Name = "INVOICE")]
        public string Invoice { get; set; }

        [MessageItem(Name = "STATUS")]
        public string Status { get; set; }

        [MessageItem(Name = "PAY_TIME")]
        public DateTime PaymentTime { get; set; }

        /// <summary>
        /// номер транзакция
        /// </summary>
        [MessageItem(Name = "STAN")]
        public string TransactionNumber { get; set; }

        /// <summary>
        /// авторизационен код на БОРИКА
        /// </summary>
        [MessageItem(Name = "BCODE")]
        public string BoricaCode { get; set; }
    }

    [CollectionMessage("Rows")]
    public class NotificationMessageResponse : NotificationMessage
    {
        public NotificationMessageResponse()
            : base()
        {
            _Rows = new List<NotificationMessageResponseRow>();
        }

        private List<NotificationMessageResponseRow> _Rows;
        /// <summary>
        /// Колекция от редове за всеки отделен Invoice в NotificationMessageResponse-а
        /// </summary>
        [Collection(ItemType = typeof(NotificationMessageResponseRow))]
        public List<NotificationMessageResponseRow> Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }
    }

    public class NotificationMessageResponseRow
    {
        [MessageItem(Name = "INVOICE")]
        [RequiredProperty(true)]
        [RegularExpression("^[0-9]{1,10}$")]
        public string Invoice { get; set; }

        [MessageItem(Name = "STATUS")]
        [RequiredProperty(true)]
        public NotificationMessageResponseStatus? Status { get; set; }
    }
}
