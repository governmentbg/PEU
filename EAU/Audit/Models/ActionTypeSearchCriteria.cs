using EAU.Common.Models;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Критерии за търсене на видове действия.
    /// </summary>
    public class ActionTypeSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на вид действие.
        /// </summary>
        public ActionTypes? ActionTypeID { get; set; }
    }
}
