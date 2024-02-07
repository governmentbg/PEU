using EAU.Reports;
using EAU.Reports.NotaryInterestsForPersonOrVehicle.Models;
using EAU.Reports.PaymentsObligations.Models;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.util;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа със справки
    /// </summary>    
    [Produces("application/json")]
    public class ReportsController : BaseApiController
    {
        private const string DEFAULT_FONT_NAME = "Arial";

        private readonly IReportsService _reportService;

        public ReportsController(IReportsService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Операция за изчитане на извършени плащания на задължения по издадени на физическо/юридическо лице фишове, наказателни постановления или споразумения.
        /// </summary>
        /// <param name="criteria">Критерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Извършени плащания на задължения</returns>
        [HttpGet]
        [Route("GetPaymentsObligations")]
        [ProducesResponseType(typeof(IEnumerable<PaymentsObligationsData>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentsObligations([FromQuery] PaymentsObligationsSearchCriteria criteria, CancellationToken cancellationToken)
        {
            if (!criteria.Page.HasValue
                || !criteria.PageSize.HasValue
                || (string.IsNullOrWhiteSpace(criteria.DebtorIdentifier) && (!criteria.DateFrom.HasValue || !criteria.DateTo.HasValue)))
            {
                throw new ArgumentNullException("Invalid search criteria.");
            }

            var state = criteria.ExtractState();
            var result = await _reportService.SearchPaymentsObligationsAsync(state, criteria, cancellationToken);

            return PagedResult(result, state);
        }

        [HttpGet]
        [Route("GetNotaryInterestsForPersonOrVehicle")]
        [ProducesResponseType(typeof(IEnumerable<NotaryInterestsForPersonOrVehicleReportData>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotaryInterestsForPersonOrVehicle([FromQuery] NotaryInterestsForPersonOrVehicleSearchCriteria criteria, CancellationToken cancellationToken)
        {
            if (!criteria.Page.HasValue || !criteria.PageSize.HasValue || !criteria.DateFrom.HasValue || !criteria.DateTo.HasValue
              || (string.IsNullOrWhiteSpace(criteria.PersonIdentifier) && string.IsNullOrWhiteSpace(criteria.VehicleRegNumber)))
            {
                throw new ArgumentNullException("Invalid search criteria.");
            }

            var state = criteria.ExtractState();
            var result = await _reportService.SearchNotaryInterestsForPersonOrVehicleAsync(state, criteria, cancellationToken);

            return PagedResult(result, state);
        }

        /// <summary>
        /// Операция за изчитане на данни за проявен интерес от нотариална кантора за физическо лице или МПС.
        /// </summary>
        /// <param name="criteria">Критерии за търсене.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Извършени плащания на задължения</returns>
        [HttpGet]
        [Route("GetNotaryInterestsForPersonOrVehicleToPDF")]
        [ProducesResponseType(typeof(IEnumerable<NotaryInterestsForPersonOrVehicleReportData>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetNotaryInterestsForPersonOrVehicleToPDF([FromQuery] NotaryInterestsForPersonOrVehicleSearchCriteria criteria, CancellationToken cancellationToken)
        {
            if (!criteria.DateFrom.HasValue || !criteria.DateTo.HasValue || (string.IsNullOrWhiteSpace(criteria.PersonIdentifier) && string.IsNullOrWhiteSpace(criteria.VehicleRegNumber)))
            {
                throw new ArgumentNullException("Invalid search criteria.");
            }

            criteria.Page = 1;
            criteria.PageSize = int.MaxValue;

            var state = criteria.ExtractState();
            var result = await _reportService.SearchNotaryInterestsForPersonOrVehicleAsync(state, criteria, cancellationToken);
            var stream = NotaryInterestsForPersonOrVehicleReportDataToPDF(criteria, result);

            return File(stream, "application/pdf", $"Справка за проявен интерес от нотариална кантора-{DateTime.Now.ToString("dd-mm-yyyy-HH-mm-ss")}.pdf");
        }

        private static Stream NotaryInterestsForPersonOrVehicleReportDataToPDF(NotaryInterestsForPersonOrVehicleSearchCriteria criteria,
            IEnumerable<NotaryInterestsForPersonOrVehicleReportData> data)
        {
            MemoryStream stream = new MemoryStream();

            #region Styles

            var styles = new StyleSheet();
            styles.LoadTagStyle(HtmlTags.BODY, "encoding", BaseFont.IDENTITY_H);
            styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.FONT, DEFAULT_FONT_NAME);

            #endregion

            #region Props

            var unicodeFontProvider = FontFactoryImp.Instance;
            unicodeFontProvider.RegisterDirectories();
            unicodeFontProvider.DefaultEmbedding = BaseFont.EMBEDDED;
            unicodeFontProvider.DefaultEncoding = BaseFont.IDENTITY_H;

            var props = new NullValueDictionary<string, object> { { "font_factory", unicodeFontProvider } };

            #endregion

            using (var document = new Document(PageSize.A4.Rotate()))
            {
                PdfWriter.GetInstance(document, stream);
                document.AddAuthor("Портал за електронни административни услуги на МВР - Административен модул");
                document.Open();
                var objects = HtmlWorker.ParseToList(new StringReader(NotaryInterestsForPersonOrVehicleReport.ToHTML(criteria, data)), styles, props);

                foreach (var element in objects)
                {
                    document.Add(element);
                }
            }

            stream.Position = 0;

            return stream;
        }
    }
}