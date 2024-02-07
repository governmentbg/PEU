using System;

namespace EAU.Common
{
    /// <summary>
    /// Грешка, описваща липса на достъп до даден ресурс
    /// </summary>
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string objectID, string objectType, int? requestUserClientID)
            : base($"Access denied for {objectType} id {objectID}, request user id: {requestUserClientID}")
        {
            ObjectID = objectID;
            ObjectType = objectType;
            RequestUserClientID = requestUserClientID;
        }

        public string ObjectID { get; private set; }
        public string ObjectType { get; private set; }
        public int? RequestUserClientID { get; private set; }
    }
}
