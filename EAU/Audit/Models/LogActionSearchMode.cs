using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Режим за търсене на записи в одит: Търсене в оперативна база = 1; Търсене в архивна база = 2.
    /// </summary>
    public enum LogActionSearchModes
    {
        /// <summary>
        /// Търсене в оперативна база = 1.
        /// </summary>
        Operational = 1,

        /// <summary>
        /// Търсене в архивна база = 2.
        /// </summary>
        Archive = 2
    }
}
