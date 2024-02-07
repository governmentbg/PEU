using EAU.Common.Cache;
using EAU.DirectoryServices.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EAU.DirectoryServices
{
    public class LDAPUserService : ILDAPUserService
    {
        private readonly IConfiguration _configuration;

        public LDAPUserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<LDAPUser> SearchUser(string user, string firstName, string surname, string lastName, Guid? objectGuid, int startIndex, int offset, ref int? cnt)
        {
            List<LDAPUser> ret = null;

            if (startIndex == 1)
            {
                using (IDisposable data = (IDisposable)SearchUser(user, firstName, surname, lastName, objectGuid))
                {
                    IEnumerable<LDAPUser> dataen = (IEnumerable<LDAPUser>)data;
                    ret = dataen.ToList();
                    cnt = ret.Count;
                }
            }

            if (ret != null)
                return ret.Skip(startIndex - 1).Take(offset);
            else
            {
                using (IDisposable data = (IDisposable)SearchUser(user, firstName, surname, lastName, objectGuid))
                {
                    return ((IEnumerable<LDAPUser>)data).Skip(startIndex - 1).Take(offset);
                }
            }
        }

        #region Helpers

        private IEnumerable<LDAPUser> SearchUser(string user, string firstName, string surname, string lastName, Guid? objectGuid)
        {
            #region check for LDAP Injection

            // check for LDAP Injection
            Regex reg = new Regex(_configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_QUERY_REGEX"));
               
            if (!String.IsNullOrEmpty(user) && reg.IsMatch(user))
                user = string.Empty;
            if (!String.IsNullOrEmpty(firstName) && reg.IsMatch(firstName))
                firstName = string.Empty;
            if (!String.IsNullOrEmpty(surname) && reg.IsMatch(surname))
                surname = string.Empty;
            if (!String.IsNullOrEmpty(lastName) && reg.IsMatch(lastName))
                lastName = string.Empty;

            #endregion

            using (DirectoryEntry de = new DirectoryEntry())
            {
                var baseDN = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_BASE_DN");

                de.Path = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_HOST");

                if (!string.IsNullOrWhiteSpace(baseDN))
                    de.Path += "/" + baseDN;

                de.AuthenticationType = AuthenticationTypes.Secure;
                de.Username = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_USERNAME");
                de.Password = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_PASSWORD"); 

                DirectorySearcher deSearch = new DirectorySearcher();

                deSearch.SearchRoot = de;

                deSearch.Filter = BuildFilter(user, firstName, surname, lastName, objectGuid);

                foreach (SearchResult sr in deSearch.FindAll())
                {
                    LDAPUser currentUser = new LDAPUser();

                    currentUser.FirstName = GetProperty<string>(sr, "givenName");
                    //// Dali sashtestvuva prezime
                    currentUser.Surname = GetProperty<string>(sr, "surname");
                    currentUser.LastName = GetProperty<string>(sr, "sn");
                    currentUser.Username = string.Format(@"{0}\{1}", _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_ACCOUNT_DOMAIN_NAME_SHORT"), GetProperty<string>(sr, "samaccountname"));
                    currentUser.Email = GetProperty<string>(sr, "mail");
                    currentUser.AccountGuid = new Guid(GetProperty<Byte[]>(sr, "objectGUID"));

                    yield return currentUser;
                }
            }
        }

        private string BuildFilter(string user, string firstName, string surname, string lastName, Guid? objectGuid)
        {
            string filter = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_FILTER");

            if (!string.IsNullOrEmpty(user))
            {
                string userNamePattern = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_USERNAME_PATTERN");
                filter += "(" + String.Format(userNamePattern, user) + ")";
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                string firstNamePattern = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_FIRSTNAME_PATTERN");
                filter += "(" + String.Format(firstNamePattern, firstName) + ")";
            }

            if (!string.IsNullOrEmpty(surname))
            {
                string surnamePattern = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_SURNAME_PATTERN");
                filter += "(" + String.Format(surnamePattern, surname) + ")";
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                string lastNamePattern = _configuration.GetEAUSection().GetValue<string>("GL_USR_LDAP_LASTNAME_PATTERN");
                filter += "(" + String.Format(lastNamePattern, lastName) + ")";
            }

            if (objectGuid.HasValue)
                filter += String.Format("(objectGuid={0})", EncodeGuid(objectGuid.Value));

            filter += ")";

            return filter;
        }

        private T GetProperty<T>(SearchResult searchResult, string PropertyName)
        {
            return (searchResult.Properties.Contains(PropertyName)) ?
                (T)searchResult.Properties[PropertyName][0] : default(T);
        }

        private string EncodeGuid(Guid guid)
        {
            StringBuilder bldr = new StringBuilder();

            foreach (byte b in guid.ToByteArray())
            {
                bldr.AppendFormat("\\{0}", BitConverter.ToString(new byte[] { b }));
            }

            return bldr.ToString();
        }

        #endregion
    }
}