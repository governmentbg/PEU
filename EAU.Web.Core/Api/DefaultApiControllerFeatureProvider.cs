using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Api
{
    /// <summary>
    /// Имплементация на IApplicationFeatureProvider&lt;ControllerFeature&gt;.
    /// Приема списък от FullName-ове на контролери, които да бъдат включени в приложението.
    /// </summary>
    public class DefaultApiControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public DefaultApiControllerFeatureProvider(params string[] enabledControllerFullNamePatterns)
        {
            EnabledControllerFullNamePatterns = enabledControllerFullNamePatterns;
        }

        private readonly string[] EnabledControllerFullNamePatterns;

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var controllersToIgnore = feature.Controllers.Where(controller => {

                byte countNotMatchedNs = 0;
                foreach (var namePattern in EnabledControllerFullNamePatterns)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(controller.FullName, namePattern))
                    {
                        countNotMatchedNs++;
                    }
                }

                return countNotMatchedNs == EnabledControllerFullNamePatterns.Length;
            }).ToList();

            foreach (var controller in controllersToIgnore)
            {
                feature.Controllers.Remove(controller);
            }
        }

        public static class ControllerFullName
        {            
            public static string BaseFullNamePattern = "^EAU.Web.Api.Controllers.*";
        }
    }
}
