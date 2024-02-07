namespace EAU.Common.Models
{
    /// <summary>
    /// Критерии за търсене на функционалности към приложението
    /// </summary>
    public class FunctionalitySearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на функционалност.
        /// </summary>
        public Functionalities? FunctionalityID { get; set; }
    }
}
