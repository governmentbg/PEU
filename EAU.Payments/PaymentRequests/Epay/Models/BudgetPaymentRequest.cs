using EAU.Payments.PaymentRequests.Epay.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    public class BudgetPaymentRequest : Message
    {
        public BudgetPaymentRequest()
            : base()
        {
        }

        private string _encoding = "CP1251";

        /// <summary>
        /// Клиентски номер
        /// </summary>
        [MessageItem(Name = "MIN")]
        [RequiredProperty(true)]
        [RegularExpression("^[a-zA-Z0-9]+[0-9]+$")]
        public string TraderNumber { get; set; }

        [MessageItem(Name = "EMAIL")]
        [RequiredProperty(true)]
        [RegularExpression(@"(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$")]
        public string Email { get; set; }

        [MessageItem(Name = "INVOICE")]
        [RequiredProperty(true)]
        [RegularExpression("^[0-9]{1,10}$")]
        public string Invoice { get; set; }

        [MessageItem(Name = "AMOUNT")]
        [RequiredProperty(true)]
        public decimal Amount { get; set; }

        [MessageItem(Name = "EXP_TIME")]
        [RequiredProperty(true)]
        [ExpirationTime]
        public DateTime ExpirationTime { get; set; }

        [MessageItem(Name = "DESCR")]
        [MaxLength(100)]
        public string Description { get; set; }

        [MessageItem(Name = "ENCODING")]
        public string Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        /// <summary>
        /// Получател на нареждането
        /// </summary>
        [MessageItem(Name = "MERCHANT")]
        [RequiredProperty(true)]
        [MaxLength(25)]
        [RegularExpression("^[a-zA-Zа-яА-Я0-9- ,.]*$")]
        public string Merchant { get; set; }

        [MessageItem(Name = "IBAN")]
        [RequiredProperty(true)]
        public string IBAN { get; set; }

        [MessageItem(Name = "BIC")]
        [RequiredProperty(true)]
        public string BIC { get; set; }

        /// <summary>
        /// Описание на плащането
        /// </summary>
        [MessageItem(Name = "STATEMENT")]
        [RequiredProperty(true)]
        [MaxLength(70)]
        [RegularExpression("^[a-zA-Zа-яА-Я0-9- ,.]*$")]
        public string PaymentDescription { get; set; }

        /// <summary>
        /// Име на задълженото лице
        /// </summary>
        [MessageItem(Name = "OBLIG_PERSON")]
        [RequiredProperty(true)]
        [MaxLength(26)]
        [RegularExpression("^[a-zA-Zа-яА-Я0-9- ,.]*$")]
        public string ObligPersonName { get; set; }

        /// <summary>
        /// Едно от трите ЕГН, БУЛСТАТ или ЛНЧ
        /// </summary>
        [RequiredProperty(true)]
        [MessageItem(DynamicNameReference = "ObligPersonType")]
        public string ObligPersonID { get; set; }

        /// <summary>
        /// Типа на идентификатора на задълженото лице
        /// </summary>
        [RequiredProperty(true)]
        public ObligPersonTypes? ObligPersonType { get; set; }

        /// <summary>
        /// Вид и номер документ
        /// </summary>
        [MessageItem(Name = "DOC_NO")]
        [RequiredProperty(true)]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата на док.
        /// </summary>
        [MessageItem(Name = "DOC_DATE")]
        [DocumentKindBaseRequired("DocumentNumber", 2, 3, 6)]
        public DateTime? DocumentDate { get; set; }

        /// <summary>
        /// Начало на период
        /// </summary>
        [MessageItem(Name = "DATE_BEGIN")]
        [DocumentKindBaseRequired("DocumentNumber", 1, 2, 4, 5)]
        public DateTime? DateBegin { get; set; }

        /// <summary>
        /// Край на период
        /// </summary>
        [MessageItem(Name = "DATE_END")]
        [DocumentKindBaseRequired("DocumentNumber", 1, 2, 4, 5)]
        public DateTime? DateEnd { get; set; }

    }

}
