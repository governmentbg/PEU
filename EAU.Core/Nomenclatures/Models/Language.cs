using EAU.Utilities;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на допустимите езици за локализация на системата.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Идентификатор на запис за език
        /// </summary>
        [DapperColumn("language_id")]
        public int? LanguageID { get; set; }

        /// <summary>
        /// код на език
        /// </summary>
        [DapperColumn("code")]
        public string Code { get; set; }

        /// <summary>
        /// наименование на език изписано на съответния език
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// флаг, указващ дали езикът е маркиран като активен
        /// </summary>
        [DapperColumn("is_active")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// флаг, указващ дали езикът е маркиран като език "по подразбиране"
        /// </summary>
        [DapperColumn("is_default")]
        public bool? IsDefault { get; set; }
    }
}
