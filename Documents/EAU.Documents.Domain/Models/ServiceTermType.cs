using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAU.Documents.Domain.Models
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/0007-000014")]
    public enum ServiceTermType
    {
        /// <summary>
        /// Обикновена
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000083")]
        Regular = 1,

        /// <summary>
        /// Бърза
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000084")]
        Fast = 2,

        /// <summary>
        /// Бърза
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000085")]
        Express = 3,
    }
}
