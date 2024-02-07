using EAU.DirectoryServices.Models;
using System;
using System.Collections.Generic;

namespace EAU.DirectoryServices
{
    /// <summary>
    /// Интерфейс за работа с потребители от активна директория.
    /// </summary>
    public interface ILDAPUserService
    {
        /// <summary>
        /// Зарежда потребители от активна директория по зададени критерии за търсене.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firstName"></param>
        /// <param name="surname"></param>
        /// <param name="lastName"></param>
        /// <param name="objectGuid"></param>
        /// <param name="startIndex"></param>
        /// <param name="offset"></param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        IEnumerable<LDAPUser> SearchUser(string user, string firstName, string surname, string lastName, Guid? objectGuid, int startIndex, int offset, ref int? cnt);
    }
}
