using CNSys;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.ServiceInstances.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Models;

namespace EAU.Services.ServiceInstances
{
    /// <summary>
    /// Заявка за създаване на Инстанция на услуга
    /// </summary>
    public class ServiceInstanceCreateRequest
    {
        /// <summary>
        /// Процеси на заявяване на услуга.
        /// </summary>
        public DocumentProcess DocumentProcess { get; set; }

        /// <summary>
        /// Данни за услугата в Бекенда.
        /// </summary>
        public ServiceInstanceInfo ServiceInstance { get; set; }
    }

    
    /// <summary>
    /// Интерфейс за работа с инстанции на услуги.
    /// </summary>
    public interface IServiceInstanceService
    {
        /// <summary>
        /// Операция за създаване на инстанция на услуга.
        /// </summary>
        /// <param name="request">Заявка за създаване на Инстанция на услуга.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Инстанция на услуга.</returns>
        Task<OperationResult<ServiceInstance>> CreateAsync(ServiceInstanceCreateRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за промяна на инстанция на услуга.
        /// </summary>
        /// <param name="backendServiceInstance">Данни за услугата в Бекенда.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Инстанция на услуга.</returns>
        Task<OperationResult<ServiceInstance>> UpdateAsync(ServiceInstanceInfo backendServiceInstance, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за търсене на инстанция на услуга.
        /// </summary>
        /// <param name="criteria">Критери за търсене.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        /// <returns></returns>
        Task<IEnumerable<ServiceInstance>> SearchAsync(ServiceInstanceSearchCriteria criteria, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за сваляне на документ.
        /// </summary>
        /// <param name="serviceInstanceID">Идентификатор на данните за услугата.</param>
        /// <param name="documentURI">УРИ на документа</param>
        /// <param name="cancellationToken">Токен за отказване</param>
        /// <returns></returns>
        Task<OperationResult<Stream>> DownloadDocumentContentAsync(long serviceInstanceID, string documentURI, CancellationToken cancellationToken);
    }
}
