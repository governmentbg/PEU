using EAU.Documents.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace EAU.Documents.Common
{
    /// <summary>
    /// Обект съдържащ подписи.
    /// </summary>
    [XmlRoot("DocumentSignatures")]
    public class DocumentSignatures
    {
        /// <summary>
        /// Списък от подписи.
        /// </summary>
        [XmlElement(ElementName = "Signature")]
        public List<Signature> Signatures { get; set; }
    }
}
