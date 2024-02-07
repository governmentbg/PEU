using EAU.DocumentTemplates.Models;
using EAU.Web.Mvc;
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
using System.Text.RegularExpressions;
using System.util;

namespace EAU.Web.Api.Private.Controllers
{
    [Route("DocumentTemplates")]
    [Authorize]
    public class DocumentTemplatesController : BaseApiController
    {
        private readonly static Lazy<Regex> _inputRegex = new Lazy<Regex>(() => new Regex("<input id=.*? size=.*? class=.*? type=.*? value=\".*?\"(/?)>"), true);
        private readonly static Lazy<Regex> _prepareInputRegex = new Lazy<Regex>(() => new Regex("<input id=.*? size=.*? class=.*? type=.*? value=\"(.*?)\"(/?)>"), true);
        private readonly static Lazy<Regex> _textareaRegex = new Lazy<Regex>(() => new Regex("<textarea class=.*? rows=.*? style=.*? value=\".*?\"((/>?)|(>.*?</textarea>))"), true);
        private readonly static Lazy<Regex> _prepareTextareaRegex = new Lazy<Regex>(() => new Regex("<textarea class=.*? rows=.*? style=.*? value=\"(.*?)\"((/>?)|(>.*?</textarea>))"), true);
        private const string DEFAULT_FONT_NAME = "Arial";

        /// <summary>
        /// Операция за създаване на документ на базата на шаблон.
        /// </summary>
        /// <param name="request">Заявка за създаване на документ.</param>
        /// <returns>Файл на създадения документа.</returns>
        [Route("CreateDocument")]
        [HttpPost]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        public IActionResult CreateDocument([FromBody] CreateDocumentRequest request)
        {
            var htmlContent = PrepareHtmlContent(request);
            var stream = PdfSharpConvert(htmlContent);

            return File(stream, "application/pdf", string.Format("{0}.pdf", request.FileName));
        }

        #region Helpers

        private static Stream PdfSharpConvert(string html)
        {
            MemoryStream ms = new MemoryStream();
            var pdfDoc = new Document(PageSize.A4);
            PdfWriter.GetInstance(pdfDoc, ms);
            pdfDoc.Open();

            PdfPCell pdfCell = new PdfPCell
            {
                Border = 0,
                RunDirection = PdfWriter.RUN_DIRECTION_LTR
            };

            var styles = new StyleSheet();
            styles.LoadTagStyle(HtmlTags.BODY, "encoding", BaseFont.IDENTITY_H);
            styles.LoadTagStyle(HtmlTags.BODY, HtmlTags.FONT, DEFAULT_FONT_NAME);
            styles.LoadTagStyle("h1", "size", "24pt");
            styles.LoadTagStyle("h2", "size", "18pt");
            styles.LoadTagStyle("h3", "size", "14pt");
            styles.LoadTagStyle("h4", "size", "12pt");
            styles.LoadTagStyle("h5", "size", "11pt");
            styles.LoadTagStyle("h6", "size", "10pt");

            var unicodeFontProvider = FontFactoryImp.Instance;
            unicodeFontProvider.RegisterDirectories();
            unicodeFontProvider.DefaultEmbedding = BaseFont.EMBEDDED;
            unicodeFontProvider.DefaultEncoding = BaseFont.IDENTITY_H;

            var props = new NullValueDictionary<string, object> {
                {"font_factory", unicodeFontProvider } // Always use Unicode fonts!
            };

            using (var reader = new StringReader(html))
            {
                var parsedHtmlElements = HtmlWorker.ParseToList(reader, styles, props);

                foreach (IElement htmlElement in parsedHtmlElements)
                {
                    pdfCell.AddElement(htmlElement);
                }
            }

            var table = new PdfPTable(1);
            table.AddCell(pdfCell);
            pdfDoc.Add(table);
            pdfDoc.Close();

            ms.Position = 0;

            return ms;
        }

        private string PrepareHtmlContent(CreateDocumentRequest request)
        {
            var htmlContent = request.HtmlTemplateContent;
            var inputs = GetInputs(htmlContent);

            foreach (var input in inputs)
            {
                htmlContent = PrepareInputValue(htmlContent, input);
            }

            var textareas = GetTextareas(htmlContent);

            foreach (var textarea in textareas)
            {
                htmlContent = PrepareTextareaValue(htmlContent, textarea);
            }

            htmlContent = string.Format("<div>{0}</div>", htmlContent);

            return htmlContent;
        }

        private List<string> GetInputs(string htmlContent)
        {
            var result = new List<string>();

            var match = _inputRegex.Value.Match(htmlContent);

            while (match != null && match.Success)
            {
                result.Add(match.Groups[0].Value);

                match = match.NextMatch();
            }

            return result;
        }

        private List<string> GetTextareas(string htmlContent)
        {
            var result = new List<string>();

            var match = _textareaRegex.Value.Match(htmlContent);

            while (match != null && match.Success)
            {
                result.Add(match.Groups[0].Value);

                match = match.NextMatch();
            }

            return result;
        }

        private string PrepareInputValue(string htmlContent, string input)
        {
            var match = _prepareInputRegex.Value.Match(htmlContent);

            if (match.Success)
            {
                var inputValue = match.Groups[1].Value;

                return htmlContent.Replace(input, inputValue);
            }

            return htmlContent;
        }

        private string PrepareTextareaValue(string htmlContent, string textarea)
        {
            var match = _prepareTextareaRegex.Value.Match(htmlContent);

            if (match.Success)
            {
                var textareaValue = match.Groups[1].Value;

                return htmlContent.Replace(textarea, textareaValue);
            }

            return htmlContent;
        }
        #endregion
    }
}