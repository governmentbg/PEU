using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Filters
{
    /// <summary>
    /// Атрибута указва да не се прилага лимит на предоставяната услуга.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class NoopServiceLimiterAttribute : Attribute, IServiceLimiterFilter

    {
    }
}
