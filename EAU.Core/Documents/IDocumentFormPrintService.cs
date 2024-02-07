using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EAU.Documents
{
    /// <summary>
    /// Интерфейс за принтиране на форма.
    /// </summary>
    public interface IDocumentFormPrintService
    {
        /// <summary>
        /// Операция за взимане на Html  за принтиране.
        /// </summary>
        /// <param name="writer">Готовият html за принтиране.</param>
        /// <param name="domainForm">Формата за принтиране.</param>
        /// <param name="appicationPath">Път на приложението.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        Task GetPrintPreviewHtmlAsync(TextWriter writer, XmlDocument domainForm, string appicationPath, CancellationToken cancellationToken);
    }
}
