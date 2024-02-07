using System.Collections.Generic;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// Заявител на електронна административна услуга.
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000016")]
    public partial class ElectronicServiceApplicant 
    {
        private List<RecipientGroup> _recipientGroups;
        private string emailAddressField;
                
        [XmlElement("RecipientGroup", typeof(RecipientGroup))]
        public List<RecipientGroup> RecipientGroups
        {
            get
            {
                if (_recipientGroups == null)
                    _recipientGroups = new List<RecipientGroup>();

                return _recipientGroups;
            }
            set
            {
                _recipientGroups = value;
            }
        }
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }
    }
    public class RecipientGroup 
    {
        private List<ElectronicStatementAuthor> _authors;
        private List<ElectronicServiceRecipient> _recipients;

        /// <summary>
        /// Автор на електронно изявление.
        /// </summary>
        [XmlElement("Author", typeof(ElectronicStatementAuthor))]
        public List<ElectronicStatementAuthor> Authors
        {
            get
            {
                if (_authors == null)
                    _authors = new List<ElectronicStatementAuthor>();

                return _authors;
            }
            set
            {
                _authors = value;
            }
        }

        /// <summary>
        /// Качество, в което авторът действа от името на титуляра и обем на представителната власт.
        /// <summary>
        public string AuthorQuality
        {
            get;
            set;
        }
        
        /// <summary>
        /// Получател на електронната административна услуга.
        /// </summary>
        [XmlElement("Recipient", typeof(ElectronicServiceRecipient))]
        public List<ElectronicServiceRecipient> Recipients
        {
            get
            {
                if (_recipients == null)
                    _recipients = new List<ElectronicServiceRecipient>();
                return _recipients;
            }
            set
            {
                _recipients = value;
            }
        }
    }
}
