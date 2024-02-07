using CNSys.Security;
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace EAU.Security
{
    /// <summary>
    /// Статичен клас, съдържащ константи за работа с потребители.
    /// </summary>
    public static class EAUClaimTypes
    {
        public const string CIN = "cin";
        public const string LoginSessionID = "login_session_id";
        public const string Subject = "sub";
        public const string UserIdentifiable = "user_identifiable";
        public const string Organization = "organization";
        public const string SpecialAccessUserType = "access_type";
        public const string HasElevatedRights = "has_elevated_rights";
        public const string PersonIdentifier = "person_identifier";
        public const string PersonIdentifierType = "person_identifier_type";
        public const string PersonNames = "person_names";
        public const string UIC = "uic";
        public const string Certificate = "certificate";
    }

    public enum PersonIdentifierTypes
    {
        EGN = 1,
        LNCh = 2,
        Other = 3
    }

    /// <summary>
    /// Клас за работа с потребители.
    /// </summary>
    public class EAUPrincipal : ClaimsPrincipal, IDataSourceUser
    {
        public const int SystemLocalUserID = 1;
        public const int AnonymousLocalUserID = 2;

        private Guid? _LoginSessionID;
        private int? _CIN;
        private string _subject;
        private bool? _IsUserIdentifiable;

        public EAUPrincipal(
            IPrincipal principal,
            string clientID = null)
            : base(principal)
        {
            ClientID = clientID;
        }

        /// <summary>
        /// Идентификатор на потребителя от локалната база данни.
        /// </summary>
        public int? LocalClientID
        {
            get
            {
                if (int.TryParse(ClientID, out int ret))
                    return ret;
                else
                    return null;
            }
        }

        /// <summary>
        /// Уникален идентификатор на login сесията на потребителя.
        /// </summary>
        public Guid? LoginSessionID
        {
            get { return _LoginSessionID.HasValue ? _LoginSessionID : _LoginSessionID = Claims.GetLoginSessionID(); }
        }

        /// <summary>
        /// Клиентски идентификационен номер.
        /// </summary>
        public int? CIN
        {
            get { return _CIN.HasValue ? _CIN : _CIN = Claims.GetCIN(); }
        }

        public string Subject
        {
            get { return _subject != null ? _subject : _subject = Claims.GetSubject(); }
        }

        /// <summary>
        /// Флаг указващ дали потребителят се е логнал със сертификат.
        /// </summary>
        public bool? IsUserIdentifiable
        {
            get { return _IsUserIdentifiable.HasValue ? _IsUserIdentifiable : _IsUserIdentifiable = Claims.GetIsUserIdentifiable(); }
        }

        /// <summary>
        /// ЕГН/ЛНЧ
        /// </summary>
        public string PersonIdentifier
        {
            get { return Claims.GetClaim(EAUClaimTypes.PersonIdentifier)?.Value; }
        }

        public PersonIdentifierTypes? PersonIdentifierType
        {
            get
            {
                var type = Claims.GetClaim(EAUClaimTypes.PersonIdentifierType)?.Value;
                switch (type)
                {
                    case "egn":
                        return PersonIdentifierTypes.EGN;
                    case "lnch":
                        return PersonIdentifierTypes.LNCh;
                    case "other":
                        return PersonIdentifierTypes.Other;
                }

                return null;
            }
        }

        /// <summary>
        /// Име от средството за автентикация
        /// </summary>
        public string PersonNames
        {
            get { return Claims.GetClaim(EAUClaimTypes.PersonNames)?.Value; }
        }

        /// <summary>
        /// ЕИК
        /// </summary>
        public string UIC
        {
            get { return Claims.GetClaim(EAUClaimTypes.UIC)?.Value; }
        }

        #region Public interface

        public string ClientID { get; private set; }

        public string ProxyUserID => null;

        #endregion
    }
}
