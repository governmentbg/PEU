using CNSys.Data;
using EAU.Audit.Models;
using EAU.Nomenclatures;
using EAU.Reports.NotaryInterestsForPersonOrVehicle.Models;
using EAU.Reports.PaymentsObligations.Models;
using EAU.Reports.Repositories;
using EAU.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Reports
{
    /// <summary>
    /// Интерфейс за работа със справки.
    /// </summary>
    public interface IReportsService
    {
        /// <summary>
        /// Справка за извършено плащане на задължения по издадени на физическо/юридическо лице фишове, наказателни постановления или споразумения
        /// </summary>
        /// <param name="state">Състояние за странициране.</param>
        /// <param name="criteria">Критери за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task<IEnumerable<PaymentsObligationsData>> SearchPaymentsObligationsAsync(PagedDataState state, PaymentsObligationsSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Справка за проявен интерес от нотариална кантора за физическо лице или МПС
        /// </summary>
        /// <param name="state">Състояние за странициране.</param>
        /// <param name="criteria">Критери за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task<IEnumerable<NotaryInterestsForPersonOrVehicleReportData>> SearchNotaryInterestsForPersonOrVehicleAsync(PagedDataState state, NotaryInterestsForPersonOrVehicleSearchCriteria criteria, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс за работа със справки.
    /// </summary>
    public class ReportsService : IReportsService
    {
        private readonly IReportsRepository _paymentsObligationsRepository;
        private readonly IUsersSearchService _usersSearchService;

        private readonly int DocTypeIdForNotaryInterestSearch;

        public ReportsService(IReportsRepository paymentsObligationsRepository, IDocumentTypes documentTypes, IUsersSearchService usersSearchService)
        {
            _paymentsObligationsRepository = paymentsObligationsRepository;
            _usersSearchService = usersSearchService;

            // Справка за проявен интерес от нотариална кантора търсим само документи "Справка за промяна на собственост на ПС V2"
            DocTypeIdForNotaryInterestSearch = documentTypes.Search().Single(d => d.Uri == "0010-003165").DocumentTypeID.Value;
        }

        #region IReportsDataService

        public Task<IEnumerable<PaymentsObligationsData>> SearchPaymentsObligationsAsync(PagedDataState state, PaymentsObligationsSearchCriteria criteria, CancellationToken cancellationToken)
        {
            return _paymentsObligationsRepository.SearchAsync(state, criteria, cancellationToken);
        }

        public async Task<IEnumerable<NotaryInterestsForPersonOrVehicleReportData>> SearchNotaryInterestsForPersonOrVehicleAsync(PagedDataState state, NotaryInterestsForPersonOrVehicleSearchCriteria criteria, CancellationToken cancellationToken)
        {
            var searchCriteria = new DocumentAccessDataReportSearchCriteria
            {
                DateFrom = criteria.DateFrom,
                DateTo = criteria.DateTo,
                DocumentTypeId = DocTypeIdForNotaryInterestSearch
            };

            if (!string.IsNullOrEmpty(criteria.PersonIdentifier))
            {
                searchCriteria.DataValue = criteria.PersonIdentifier;
                searchCriteria.DataType = (byte?)DocumentAccessedDataTypes.PersonIdentifier;
            }
            else if (!string.IsNullOrEmpty(criteria.VehicleRegNumber))
            {
                searchCriteria.DataValue = criteria.VehicleRegNumber;
                searchCriteria.DataType = (byte?)DocumentAccessedDataTypes.VehicleRegNumber;
            }

            var groupedData = await _paymentsObligationsRepository.SearchAsync(state, searchCriteria, cancellationToken);

            var userIds = groupedData.Select(d => d.UserId.Value).Distinct().ToList();

            var users = await _usersSearchService.SearchUsersAsync(new Users.Models.UserSearchCriteria { UserIDs = userIds }, cancellationToken);

            var report = new List<NotaryInterestsForPersonOrVehicleReportData>();

            foreach (var documentAccessedData in groupedData)
            {
                var dataValues = new List<DocumentAccessedDataValue>();

                foreach (var dataPart in documentAccessedData.GroupData.Split('|'))
                {
                    var items = dataPart.Split(':');
                    dataValues.Add(new DocumentAccessedDataValue { DataType = (DocumentAccessedDataTypes)byte.Parse(items[0]), DataValue = items[1] });
                }

                var reportRow = new NotaryInterestsForPersonOrVehicleReportData
                {
                    DocumentAccessedDataValues = dataValues.ToArray(),
                    InterestDate = documentAccessedData.DateOn,
                    IPAddress = new System.Net.IPAddress(documentAccessedData.IpAddress).ToString(),
                    NotaryUserEmail = users.SingleOrDefault(u => u.UserID == documentAccessedData.UserId)?.Email,
                    NotaryUserNames = documentAccessedData.ApplicantNames,
                    NotaryUserIdentifier = documentAccessedData.ApplicantIdentifier
                };

                report.Add(reportRow);
            }

            return report;
        }

        #endregion
    }
}