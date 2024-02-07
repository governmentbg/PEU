using EAU.Payments.PaymentRequests.Epay.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Models
{
    public class Message
    {
        #region Properties

        private static readonly string _dateTimeFormatString = "dd.MM.yyyy HH:mm";
        private static readonly string _decimalFormatString = "0.00";
        private static readonly char _separator = '\n';
        private static readonly Encoding _encoding = Encoding.GetEncoding(1251);

        protected string dateTimeFormatString { get; set; }
        protected string decimalFormatString { get; set; }
        protected char separator { get; set; }
        protected Encoding encoding { get; set; }

        #endregion

        #region Constructor

        public Message()
        {
            dateTimeFormatString = _dateTimeFormatString;
            decimalFormatString = _decimalFormatString;
            separator = _separator;
            encoding = _encoding;
        }

        #endregion

        public string SaveMessageObject(bool encodeBeforeSave)
        {
            MessageValidator validator = new MessageValidator(this);

            string validationResult = validator.ValidateMessage();

            if (!string.IsNullOrEmpty(validationResult))
                throw new ArgumentException("Payment message is not valid: \r\n" + validationResult);

            string ret = ToMessageString();
            if (encodeBeforeSave)
                return EncodeBase64String(ret);
            else
                return ret;
        }

        public void LoadMessageObject(string message, bool decodeBeforeLoad)
        {
            string msg = message;

            if (decodeBeforeLoad)
                msg = DecodeFromBase64String(message);

            LoadFormString(msg);
        }

        public string ToMessageString()
        {
            StringBuilder result = new StringBuilder();

            var classAttribute = (CollectionMessageAttribute)this.GetType().GetCustomAttribute(typeof(CollectionMessageAttribute));

            if (classAttribute != null)
            {
                Type typeObj = this.GetType();

                foreach (var property in typeObj.GetProperties())
                {
                    object tempPropertyValue = property.GetValue(this);

                    IList ilistObj = tempPropertyValue as IList;
                    if (ilistObj != null && !ilistObj.GetType().IsArray)
                    {
                        foreach (var item in ilistObj)
                        {
                            result.Append(CreateString(item));
                            result.Append("\n");
                        }
                    }
                }
            }
            else
            {
                result.Append(CreateString(this));
            }

            return result.ToString().TrimEnd(new char[] { '\n' });
        }

        public void LoadFormString(string result)
        {
            List<string> results = result.TrimEnd('\n').Split(new char[] { '\n' }).ToList();

            Type typeObj = this.GetType();
            var classAttribute = (CollectionMessageAttribute)typeObj.GetCustomAttribute(typeof(CollectionMessageAttribute));

            if (classAttribute != null)
            {
                foreach (var property in typeObj.GetProperties())
                {
                    object tempPropertyValue = property.GetValue(this);

                    IList ilistObj = tempPropertyValue as IList;
                    if (ilistObj != null
                        && !ilistObj.GetType().IsArray)
                    {
                        CollectionAttribute collectionAttribute = (CollectionAttribute)property.GetCustomAttribute(typeof(CollectionAttribute));

                        if (collectionAttribute != null)
                        {
                            foreach (var s in results)
                            {
                                object element = Activator.CreateInstance(collectionAttribute.ItemType);

                                Load(s, element);

                                ilistObj.Add(element);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var s in results)
                {
                    Load(result, this);
                }
            }
        }

        #region Helpers

        private string CreateString(object message)
        {
            StringBuilder result = new StringBuilder();

            Type typeObj = message.GetType();

            foreach (var property in typeObj.GetProperties())
            {
                MessageItemAttribute messageItemAttr = (MessageItemAttribute)property.GetCustomAttribute(typeof(MessageItemAttribute));

                if (messageItemAttr != null)
                {
                    if (!String.IsNullOrEmpty(messageItemAttr.DynamicNameReference) &&
                        !String.IsNullOrEmpty(messageItemAttr.Name))
                        throw new NotSupportedException("Cannot have both set Name and DynamicNameReference ");

                    string pName = null;

                    if (String.IsNullOrEmpty(messageItemAttr.DynamicNameReference) &&
                        String.IsNullOrEmpty(messageItemAttr.Name))
                    {
                        pName = property.Name;
                    }
                    else
                        pName = messageItemAttr.Name;

                    var pValue = property.GetValue(message);

                    if (pValue != null)
                    {
                        string pMessageItemName =
                            !String.IsNullOrEmpty(pName) ?
                            pName :
                            typeObj.GetProperty(messageItemAttr.DynamicNameReference).GetValue(message).ToString();

                        string formatedData = FormatMessageItem(pValue);

                        if (formatedData != null)
                            result.Append(pMessageItemName + "=" + formatedData + separator);
                    }
                }
            }

            return result.ToString().TrimEnd(new char[] { separator });
        }

        private void Load(string messagerow, object Obj)
        {
            if (string.IsNullOrEmpty(messagerow))
                throw new ArgumentNullException("messagerow must not be empty !");

            var pvArr = messagerow.Split(':');

            Dictionary<string, string> messageData = new Dictionary<string, string>();

            foreach (var item in pvArr)
            {
                var tmpValue = item.Split('=');
                messageData.Add(tmpValue[0], tmpValue[1]);
            }

            var typeObj = Obj.GetType();

            foreach (var property in typeObj.GetProperties())
            {
                var messageItemAttr = ((MessageItemAttribute)property.GetCustomAttribute(typeof(MessageItemAttribute)));

                if (!String.IsNullOrEmpty(messageItemAttr.DynamicNameReference) &&
                        !String.IsNullOrEmpty(messageItemAttr.Name))
                    throw new NotSupportedException("Cannot have both set Name and DynamicNameReference ");

                string pName = null;

                if (String.IsNullOrEmpty(messageItemAttr.DynamicNameReference) &&
                    String.IsNullOrEmpty(messageItemAttr.Name))
                {
                    pName = property.Name;
                }
                else
                    pName = messageItemAttr.Name;

                if (!String.IsNullOrEmpty(pName))
                {
                    string pValue;

                    if (messageData.TryGetValue(pName, out pValue))
                    {
                        property.SetValue(Obj, ParseMessageItem(pValue, property.PropertyType));

                        messageData.Remove(pName);
                    }
                }
                else
                {
                    var dynamicReferenceProprty = typeObj.GetProperty(messageItemAttr.DynamicNameReference);

                    Type dynamicNames = dynamicReferenceProprty.PropertyType;

                    foreach (var item in Enum.GetValues(dynamicNames))
                    {
                        string pValue;

                        if (messageData.TryGetValue(item.ToString(), out pValue))
                        {
                            property.SetValue(Obj, ParseMessageItem(pValue, property.PropertyType));
                            messageData.Remove(pName);

                            dynamicReferenceProprty.SetValue(Obj, item);

                            break;
                        }
                    }
                }
            }
        }

        private string FormatMessageItem(object itemData)
        {
            if (itemData.GetType() == typeof(DateTime))
            {
                if ((DateTime)itemData != DateTime.MinValue)
                    return ((DateTime)itemData).ToString(dateTimeFormatString);
            }
            else if (itemData.GetType() == typeof(decimal))
            {
                if ((decimal)itemData != decimal.MinValue)
                    return ((decimal)itemData).ToString(decimalFormatString, System.Globalization.CultureInfo.InvariantCulture);
            }
            else if (itemData.GetType() == typeof(string))
            {
                if (!string.IsNullOrEmpty((string)itemData))
                    return itemData.ToString();
            }
            else if (itemData.GetType() == typeof(NotificationMessageResponseStatus))
            {
                return itemData.ToString();
            }
            else
                throw new NotImplementedException();

            return null;
        }

        private object ParseMessageItem(string itemData, Type itemDataType)
        {
            if (itemDataType == typeof(DateTime))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                DateTime resDate = DateTime.ParseExact(itemData, dateTimeFormatString, provider);

                return resDate;
            }
            else if (itemDataType == typeof(decimal))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                decimal resDecimal = Convert.ToDecimal(itemData, provider);

                return resDecimal;
            }
            else if (itemDataType == typeof(string))
            {
                return itemData;
            }
            else
                throw new NotSupportedException();
        }

        private string EncodeBase64String(string toEncode)
        {
            if (toEncode == null)
                return toEncode;

            return Convert.ToBase64String(encoding.GetBytes(toEncode));
        }

        private string DecodeFromBase64String(string encodedData)
        {
            if (encodedData == null)
                return encodedData;

            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            return encoding.GetString(encodedDataAsBytes);
        }

        #endregion
    }
}
