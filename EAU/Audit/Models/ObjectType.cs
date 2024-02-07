using EAU.Utilities;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Клас за работа с видове обекти.
    /// </summary>
    public class ObjectType
    {
        /// <summary>
        /// Уникален идентификатор на вид обект.
        /// </summary>
        [DapperColumn("object_type_id")]
        public ObjectTypes? ObjectTypeID { get; set; }

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
