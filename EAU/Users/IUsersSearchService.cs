using EAU.Users.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    public interface IUsersSearchService
    {
        /// <summary>
        /// Търсене на потребители.
        /// </summary>
        /// <param name="usersSearchCriteria"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> SearchUsersAsync(UserSearchCriteria usersSearchCriteria, CancellationToken cancellationToken);

        /// <summary>
        /// Търсене на логин сесия.
        /// </summary>
        /// <param name="searchCriteria">Критерии за търсене.</param>
        /// <param name="LoadUser">Флаг указващ дали да се зареди потребител.</param>
        /// <param name="LoadCertificate">Флаг указващ дали да се зареди сертификат.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Логин сесии.</returns>
        Task<IEnumerable<UserLoginSession>> SearchLoginSessionAsync(UserLoginSessionSearchCriteria searchCriteria, bool? LoadUser, bool? LoadCertificate, CancellationToken cancellationToken);
    }
}
