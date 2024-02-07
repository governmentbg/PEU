using EAU.Utilities;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Клас за работа с видове действия.
    /// </summary>
    public class ActionType
    {
        /// <summary>
        /// Уникален идентификатор на вид действие.
        /// </summary>
        [DapperColumn("action_type_id")]
        public ActionTypes? ActionTypeID { get; set; }

        /// <summary>
        /// Име.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }
    }
}
