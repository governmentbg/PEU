using EAU.Common.Models;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Критерии за търсене на видове обекти.
    /// </summary>
    public class ObjectTypeSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на вид обекти.
        /// </summary>
        public ObjectTypes? ObjectTypeID { get; set; }
    }
}
