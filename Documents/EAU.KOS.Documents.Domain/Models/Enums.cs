namespace EAU.KOS.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2202")]
	public enum WeaponNoticeType
	{
		/// <summary>
		/// Придобито
		/// </summary>
		[System.Xml.Serialization.XmlEnumAttribute("2001")]
		Acquired,

		/// <summary>
		/// Прехвърлено
		/// </summary>
		[System.Xml.Serialization.XmlEnumAttribute("2002")]
		Transferred,

        /// <summary>
        /// Продажба/дарение
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2003")]
        SaleDonation,
    }
}
