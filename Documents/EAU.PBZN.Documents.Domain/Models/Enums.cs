using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.PBZN.Documents.Domain.Models
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1101")]
	public enum CertificateType
	{
		[System.Xml.Serialization.XmlEnumAttribute("2001")]
		Issuing,
		[System.Xml.Serialization.XmlEnumAttribute("2002")]
		Renewing,
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1102")]
	public enum EntityOrPerson
	{
		[System.Xml.Serialization.XmlEnumAttribute("2001")]
		Entity,
		[System.Xml.Serialization.XmlEnumAttribute("2002")]
		Person,
	}
}
