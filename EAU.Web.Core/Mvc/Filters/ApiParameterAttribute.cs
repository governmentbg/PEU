using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Mvc.Filters
{
    public class ApiParameterAttribute : Attribute, IApiDescriptionFilter
    {
        public ApiParameterAttribute()
        {
        }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public string Source { get; set; }
        public Type Type { get; set; }

        public void Process(ApiDescription apiDescription)
        {
            BindingSource bindingSource = BindingSource.Query;

            switch (Source.ToLower())
            {
                case "body":
                    bindingSource = BindingSource.Body;
                    break;
                case "custom":
                    bindingSource = BindingSource.Custom;
                    break;
                case "form":
                    bindingSource = BindingSource.Form;
                    break;
                case "formfile":
                    bindingSource = BindingSource.FormFile;
                    break;
                case "header":
                    bindingSource = BindingSource.Header;
                    break;
                case "modelbinding":
                    bindingSource = BindingSource.ModelBinding;
                    break;
                case "path":
                    bindingSource = BindingSource.Path;
                    break;
                case "query":
                    bindingSource = BindingSource.Query;
                    break;
                case "services":
                    bindingSource = BindingSource.Services;
                    break;
                case "special":
                    bindingSource = BindingSource.Special;
                    break;
                default:
                    throw new NotImplementedException();
            }


            apiDescription.ParameterDescriptions.Add(new ApiParameterDescription() { Name = Name, Type = Type, Source = bindingSource, IsRequired = IsRequired });
        }
    }
}
