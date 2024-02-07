namespace EAU.DocumentTemplates.Models
{
    //TODO Joro: да се премести в private api
    /// <summary>
    /// Заявка за създаване на документ.
    /// </summary>
    public class CreateDocumentRequest
    {
        /// <summary>
        /// HTML съдържание на шаблона.
        /// </summary>
        public string HtmlTemplateContent { get; set; }

        /// <summary>
        /// Наименование на файл.
        /// </summary>
        public string FileName { get; set; }
    }
}
