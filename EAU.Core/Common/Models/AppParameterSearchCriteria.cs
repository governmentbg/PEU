namespace EAU.Common.Models
{
    /// <summary>
    /// Критерии за търсене на параметри към приложението
    /// </summary>
    public class AppParameterSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на функционалност.
        /// </summary>
        public Functionalities? FunctionalityID { get; set; }

        /// <summary>
        /// Код на параметър.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Флаг указващ дали да търси по пълно съвпадение на Код.
        /// </summary>
        public bool? CodeIsExact { get; set; }

        /// <summary>
        /// Описание на параметър.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Флаг, указващ дали параметъра е системен или не.
        /// </summary>
        public bool? IsSystem { get; set; }
    }
}
