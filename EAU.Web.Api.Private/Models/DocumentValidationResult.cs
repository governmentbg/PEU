using System.Collections.Generic;

namespace EAU.Web.Api.Private.Models
{
    public class DocumentValidationResult
    {
        public class Error
        {
            public string Code { get; set; }
            public string Message { get; set; }
        }

        public bool IsValid { get; set; }

        public List<Error> Errors { get; set; }
    }
}
