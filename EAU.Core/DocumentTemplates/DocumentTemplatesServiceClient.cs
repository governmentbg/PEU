using CNSys.IO;
using EAU.DocumentTemplates.Models;
using EAU.Utilities;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.DocumentTemplates
{
    /// <summary>
    /// Интерфейс на http клиент за работа с шаблони на документи.
    /// </summary>
    public interface IDocumentTemplatesServiceClient
    {
        Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request);
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentTemplatesServiceClient за работа с шаблони на документи.
    /// </summary>
    internal class DocumentTemplatesServiceClient : IDocumentTemplatesServiceClient
    {
        private readonly HttpClient _client;

        public DocumentTemplatesServiceClient(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException("client");
        }

        public async Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request)
        {
            var disposeResult = false;
            HttpResponseMessage result = null;
            /*При наличие на грешка, освобождаваме result - a */
            try
            {
                result = await _client.PostAsync("DocumentTemplates/CreateDocument", request, CancellationToken.None);

                result.EnsureSuccessStatusCode2();

                return new CreateDocumentResponse()
                {
                    Content = new DisposingStream(await result.Content.ReadAsStreamAsync(),
                        () => { result.Dispose(); }),
                    FileName = !string.IsNullOrEmpty(result.Content.Headers.ContentDisposition.FileNameStar) ? result.Content.Headers.ContentDisposition.FileNameStar : result.Content.Headers.ContentDisposition.FileName,
                    ContentType = result.Content.Headers.ContentType.MediaType
                };
            }
            catch
            {
                disposeResult = true;
                throw;
            }
            finally
            {
                if (disposeResult)
                    result?.Dispose();
            }
        }
    }
}
