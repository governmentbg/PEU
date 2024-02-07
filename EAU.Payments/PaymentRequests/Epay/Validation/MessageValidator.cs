using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.Text.RegularExpressions;
using System.ServiceModel.Channels;
using EAU.Payments.PaymentRequests.Epay.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace EAU.Payments.PaymentRequests.Epay.Validation
{
    public class MessageValidator
    {
        private Models.Message paymentMssage;

        public MessageValidator(Models.Message paymentMssage)
        {
            this.paymentMssage = paymentMssage;
        }

        public string ValidateMessage()
        {
            StringBuilder result = new StringBuilder();

            Type typeObj = paymentMssage.GetType();
            var classAttribute = (CollectionMessageAttribute)typeObj.GetCustomAttribute(typeof(CollectionMessageAttribute));

            if (classAttribute != null)
            {
                foreach (var property in typeObj.GetProperties())
                {
                    object tempPropertyValue = property.GetValue(paymentMssage);

                    IList ilistObj = tempPropertyValue as IList;
                    if (ilistObj != null && !ilistObj.GetType().IsArray)
                    {
                        foreach (var item in ilistObj)
                        {
                            result.Append(this.Validate(item));
                        }
                    }
                }
            }
            else
            {
                result.Append(this.Validate(paymentMssage));
            }

            return result.ToString();
        }

        #region Helper

        private string Validate(object Obj)
        {
            StringBuilder result = new StringBuilder();

            foreach (var property in Obj.GetType().GetProperties())
            {
                if (property.GetCustomAttribute(typeof(RequiredPropertyAttribute)) != null)
                {
                    bool required = ((RequiredPropertyAttribute)property.GetCustomAttribute(typeof(RequiredPropertyAttribute))).Required;

                    if (required)
                    {
                        var pValue = property.GetValue(Obj);

                        if (property.PropertyType == typeof(DateTime))
                        {
                            if (pValue == null || (DateTime)pValue == DateTime.MinValue)
                                result.Append("Property " + property.Name + " is required!" + Environment.NewLine);
                        }
                        else if (property.PropertyType == typeof(decimal))
                        {
                            if (pValue == null || (decimal)pValue == decimal.MinValue)
                                result.Append("Property " + property.Name + " is required!" + Environment.NewLine);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            if (string.IsNullOrEmpty((string)pValue))
                                result.Append("Property " + property.Name + " is required!" + Environment.NewLine);
                        }
                        else if (property.PropertyType == typeof(NotificationMessageResponseStatus?))
                        {
                            if (!((NotificationMessageResponseStatus?)pValue).HasValue)
                                result.Append("Property " + property.Name + " is required!" + Environment.NewLine);
                        }
                    }
                }

                if (property.GetCustomAttribute(typeof(MaxLengthAttribute)) != null)
                {
                    int maxLength = ((MaxLengthAttribute)property.GetCustomAttribute(typeof(MaxLengthAttribute))).Length;

                    if (property.PropertyType == typeof(string))
                    {
                        var pValue = property.GetValue(Obj);

                        if (!string.IsNullOrEmpty((string)pValue) && ((string)pValue).Length > maxLength)
                            result.Append("Property " + property.Name + " max length is " + maxLength.ToString() + "!" + Environment.NewLine);
                    }
                }

                if (property.GetCustomAttribute(typeof(RegularExpressionAttribute)) != null)
                {
                    string pattern = ((RegularExpressionAttribute)property.GetCustomAttribute(typeof(RegularExpressionAttribute))).Pattern;

                    if (property.PropertyType == typeof(string))
                    {
                        var pValue = property.GetValue(Obj);

                        if (string.IsNullOrEmpty((string)pValue) || !Regex.IsMatch((string)pValue, pattern))
                            result.Append("Property " + property.Name + " doesn't match pattern " + pattern + "!" + Environment.NewLine);
                    }
                }

                if (property.GetCustomAttribute(typeof(ExpirationTimeAttribute)) != null)
                {
                    if (property.PropertyType == typeof(DateTime))
                    {
                        var pValue = property.GetValue(Obj);

                        if (pValue != null && (DateTime)pValue > DateTime.Now.Date.AddDays(30))
                            result.Append("Property " + property.Name + " expiration time is more then 30 days!" + Environment.NewLine);
                    }
                }

                if (property.GetCustomAttribute(typeof(DocumentKindBaseRequiredAttribute)) != null)
                {
                    int[] documentKinds = ((DocumentKindBaseRequiredAttribute)property.GetCustomAttribute(typeof(DocumentKindBaseRequiredAttribute))).DocumentKinds;
                    string documentKindProperty = ((DocumentKindBaseRequiredAttribute)property.GetCustomAttribute(typeof(DocumentKindBaseRequiredAttribute))).DocumentKindProperty;

                    string documentKind = Obj.GetType().GetProperty(documentKindProperty).GetValue(Obj).ToString().Substring(0, 1);

                    if (documentKinds.Contains(Convert.ToInt32(documentKind)))
                    {
                        var pValue = property.GetValue(Obj);

                        if (property.PropertyType == typeof(DateTime))
                        {
                            if (pValue == null || (DateTime)pValue == DateTime.MinValue)
                                result.Append("Property " + property.Name + " is required for this document kind " + documentKind + "!" + Environment.NewLine);
                        }
                        else if (property.PropertyType == typeof(decimal))
                        {
                            if (pValue == null || (decimal)pValue == decimal.MinValue)
                                result.Append("Property " + property.Name + " is required for this document kind " + documentKind + "!" + Environment.NewLine);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            if (string.IsNullOrEmpty((string)pValue))
                                result.Append("Property " + property.Name + " is required for this document kind " + documentKind + "!" + Environment.NewLine);
                        }
                    }
                }
            }

            return result.ToString();
        }

        #endregion
    }
}
