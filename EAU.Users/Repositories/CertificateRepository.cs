using CNSys.Data;
using EAU.Common;
using EAU.Data;
using EAU.Users.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип Certificate.
    /// </summary>
    public interface ICertificateRepository :
        IRepository<Certificate, long?, CertificateSearchCriteria>,
        IRepositoryAsync<Certificate, long?, CertificateSearchCriteria>
    {
    }

    /// <summary>
    /// Критерии за търсене на сертификати.
    /// </summary>
    public class CertificateSearchCriteria
    {
        /// <summary>
        /// Идентификатори на сертификатори.
        /// </summary>
        public List<int> CertificateIDs { get; set; }

        /// <summary>
        /// Хеш на сертификата.
        /// </summary>
        public string CertHash { get; set; }

        /// <summary>
        /// Сериен номер на сертификата.
        /// </summary>
        public string CertSerialNumber { get; set; }

        /// <summary>
        /// Флаг, указващ дали да се зареди съдържанието на сертификата.
        /// </summary>
        public bool LoadContent { get; set; }
    }

    /// <summary>
    /// Реализация на интерфейс ICertificateRepository за поддържане и съхранение на обекти от тип Certificate.
    /// </summary>
    internal class CertificateRepository : EAURepositoryBase<Certificate, long?, CertificateSearchCriteria, CertificateDataContext>, ICertificateRepository
    {
        #region Constructors

        public CertificateRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        protected override async Task CreateInternalAsync(CertificateDataContext context, Certificate item, CancellationToken cancellationToken)
        {
            item.CertificateID = await context.CertificatesCreateAsync(item.SerialNumber, item.Issuer, item.Subject, item.NotAfter, item.NotBefore, item.CertHash, item.Content, cancellationToken);
        }

        protected override Task<IEnumerable<Certificate>> SearchInternalAsync(CertificateDataContext context, CertificateSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<Certificate>> SearchInternalAsync(CertificateDataContext context, PagedDataState state, CertificateSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var res = await context.CertificatesSearchAsync(searchCriteria.CertificateIDs.ConcatItems(), searchCriteria.CertHash, searchCriteria.CertSerialNumber, searchCriteria.LoadContent, cancellationToken);
            return res.ToList();
        }

        #endregion
    }
}
