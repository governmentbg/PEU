using System;

namespace EAU.Common
{
    /// <summary>
    /// Грешка, която описва липса на данни или ресурс
    /// </summary>
    public class NoDataFoundException : Exception
    {
        public NoDataFoundException(string objectID, string objectType) : base($"No data found for {objectType} id {objectID}")
        {
            ObjectID = objectID;
            ObjectType = objectType;
        }

        public string ObjectID { get; private set; }
        public string ObjectType { get; private set; }
    }    
}
