using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Mvc
{
    /// <summary>
    /// За предпочитане в value да бъде LINQ заявка, която да не е материализирана, 
    /// защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!
    /// </summary>
    public class ObjectResultWithETag : ObjectResult
    {
        /// <summary>
        /// За предпочитане в value да бъде LINQ заявка, която да не е материализирана, 
        /// защото в повечето случаи няма да трябва да се изпълнява. Ще се връща 304!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objectVersion"></param>
        public ObjectResultWithETag(object value, string objectVersion) : base(value)
        {
            ObjectVersion = objectVersion;
        }

        public ObjectResultWithETag(Func<object> valueGenerator, string objectVersion) : base(null)
        {
            ObjectVersion = objectVersion;
            ValueGenerator = valueGenerator;
        }

        private string ObjectVersion { get; set; }

        private Func<object> ValueGenerator { get; set; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            string etag = string.Format("\"{0}\"", ObjectVersion);
            context.HttpContext.Response.Headers.Add("ETag", new string[] { etag });

            context.HttpContext.Response.StatusCode =
                (context.HttpContext.Request.Headers.TryGetValue("If-None-Match", out StringValues value) && value.SingleOrDefault() == etag) ?
                StatusCodes.Status304NotModified : StatusCodes.Status200OK;

            if (context.HttpContext.Response.StatusCode != StatusCodes.Status304NotModified)
            {
                if (ValueGenerator != null)
                    Value = ValueGenerator();

                return base.ExecuteResultAsync(context);
            }
            else
                return Task.CompletedTask;
        }
    }
}
