using EAU.Audit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAU.Reports.NotaryInterestsForPersonOrVehicle.Models
{
    public static class NotaryInterestsForPersonOrVehicleReport
    {
        public static string ToHTML(NotaryInterestsForPersonOrVehicleSearchCriteria criteria, IEnumerable<NotaryInterestsForPersonOrVehicleReportData> data)
        {
            var sb = new StringBuilder();

            if (criteria.DateFrom.HasValue && criteria.DateTo.HasValue)
            {
                if (!string.IsNullOrWhiteSpace(criteria.PersonIdentifier) && !string.IsNullOrWhiteSpace(criteria.VehicleRegNumber))
                {
                    sb.Append($"<p style='font-size:10px;'>СПРАВКА ЗА ПРОЯВЕН ИНТЕРЕС КЪМ ЛИЦЕ БЪЛГАРСКИ ГРАЖДАНИН С ЕГН <b>{criteria.PersonIdentifier}</b> И МПС С РЕГИСТРАЦИОНЕН НОМЕР <b>{criteria.VehicleRegNumber}</b> В ПЕРИОДА ОТ <b>{criteria.DateFrom.Value.ToString("dd.MM.yyyy г.")}</b> ДО <b>{criteria.DateTo.Value.ToString("dd.MM.yyyy г.")}</b></p>");
                }
                else if (!string.IsNullOrWhiteSpace(criteria.PersonIdentifier))
                {
                    sb.Append($"<p style='font-size:10px;'>СПРАВКА ЗА ПРОЯВЕН ИНТЕРЕС КЪМ ЛИЦЕ БЪЛГАРСКИ ГРАЖДАНИН С ЕГН <b>{criteria.PersonIdentifier}</b> В ПЕРИОДА ОТ <b>{criteria.DateFrom.Value.ToString("dd.MM.yyyy г")}</b> ДО <b>{criteria.DateTo.Value.ToString("dd.MM.yyyy г.")}</b></p>");
                }
                else if (!string.IsNullOrWhiteSpace(criteria.VehicleRegNumber))
                {
                    sb.Append($"<p style='font-size:10px;'>СПРАВКА ЗА ПРОЯВЕН ИНТЕРЕС КЪМ МПС С РЕГИСТРАЦИОНЕН НОМЕР <b>{criteria.VehicleRegNumber}</b> В ПЕРИОДА ОТ <b>{criteria.DateFrom.Value.ToString("dd.MM.yyyy г.")}</b> ДО <b>{criteria.DateTo.Value.ToString("dd.MM.yyyy г.")}</b></p>");
                }
            }

            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append("<table style='width: 100%;font-size:8px;'>");
            sb.Append("<thead><tr>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1' colspan='3'>Нотариус изпълнил справката</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1'>Лице към което е проявен интерес</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1' rowspan='2'>МПС към което е проявен интерес (регистрационен номер)</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1' rowspan='2'>Работно място (IP address)</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1' rowspan='2'>Време на справката</th>");
            sb.Append("</tr><tr>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1'>ЕГН/ЛНЧ/ЛН</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1'>Име</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1'>Адрес на електронна поща</th>");
            sb.Append("<th valign='center' align='center' bgcolor='#d9d9d9' border='1'>ЕГН/ЛНЧ/ЛН и име</th>");
            sb.Append("</tr></thead>");
            sb.Append("<tbody>");

            foreach (var currentRowData in data)
            {
                sb.Append("<tr>");
                sb.Append($"<td valign='center' align='center' border='1'>{currentRowData.NotaryUserIdentifier}</td>");
                sb.Append($"<td valign='center' align='center' border='1'>{currentRowData.NotaryUserNames}</td>");
                sb.Append($"<td valign='center' align='center' border='1'>{currentRowData.NotaryUserEmail}</td>");
                sb.Append($"<td valign='center' align='center' border='1'>{GetDocumentAccessedDataValuesRowsByType(currentRowData.DocumentAccessedDataValues, DocumentAccessedDataTypes.PersonIdentifierAndNames)}</td>");
                sb.Append($"<td valign='center' align='center' border='1'>{GetDocumentAccessedDataValuesRowsByType(currentRowData.DocumentAccessedDataValues, DocumentAccessedDataTypes.VehicleRegNumber)}</td>");
                sb.Append($"<td valign='center' align='center' border='1'>{currentRowData.IPAddress}</td>");
                sb.Append($"<td valign='center' align='center' border='1'>{currentRowData.InterestDate?.ToString("dd.MM.yyyy г HH:mm:ss ч")}</td>");
                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</table>");

            sb.Append("<br/>");
            sb.Append("<br/>");
            sb.Append($"<p align='right'>{DateTime.Now.ToString("dd.MM.yyyy г.")}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ДКИС<p>");

            return sb.ToString();
        }

        private static string GetDocumentAccessedDataValuesRowsByType(DocumentAccessedDataValue[] data, DocumentAccessedDataTypes type)
        {
            if (data?.Length > 0)
            {
                var filteredvalues = data.Where(x => x.DataType == type).Select(x => x.DataValue);

                if (filteredvalues.Any())
                {
                    var sb = new StringBuilder();

                    foreach (var currentValue in filteredvalues)
                    {
                        sb.Append($"<p>{currentValue}</p>");

                    }

                    return sb.ToString();
                }

                return "";
            }

            return "";
        }
    }
}