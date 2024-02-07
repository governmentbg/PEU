using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace EAU.Documents.XSLT
{
    public class XSLExtensions
    {
        public string HtmlDecode(string encodedHTML)
        {
            return WebUtility.HtmlDecode(encodedHTML);
        }

        public string HtmlEncode(string decodedHTML)
        {
            return WebUtility.HtmlEncode(decodedHTML);
        }

        public string ExtractFirstGroup(string text, string regex)
        {
            return new Regex(regex).Match(text).Groups[1].Value;
        }

        public string FormatDate(string date, string format)
        {
            DateTimeOffset dateOffsVal;
            if (DateTimeOffset.TryParse(date, out dateOffsVal))
            {
                return dateOffsVal.ToString(format);
            }
            else
            {
                return date;
            }
        }
    }
}