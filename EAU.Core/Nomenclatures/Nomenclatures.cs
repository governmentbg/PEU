using EAU.Common.Models;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures
{
    public interface ILoadable
    {
        ValueTask EnsureLoadedAsync(CancellationToken cancellationToken);
    }

    /// <summary>
    /// Интерфейс за работа с етикети.
    /// </summary>
    public interface ILabels : ILoadable
    {
        /// <summary>
        /// Метод за достигане на всички ресурси по даден език.
        /// </summary>
        /// <param name="lang">Език, за който да бъдат върнати всички ресурси.</param>
        /// <param name="lastModifiedDate"></param>
        /// <returns>Връща всички ресурси</returns>
        IEnumerable<Label> Search(string lang, out DateTime? lastModifiedDate);

        ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Интерфейс за работа с етикети.
    /// </summary>
    public interface ILabelLocalizations : ILoadable
    {        
        /// <summary>
        /// Метод за достигане на ресурс по даден език.
        /// </summary>
        /// <param name="lang">Език, на който да бъдат върнати ресурсите.</param>
        /// <param name="labelCode">Код на етикета.</param>
        /// <returns>Връща ресурс по даден език и код на етикет.</returns>
        string Get(string lang, string labelCode);

        /// <summary>
        /// Метод за достигане на всички ресурси по даден език.
        /// </summary>
        /// <param name="lang">Език, за който да бъдат върнати всички ресурси.</param>
        /// <param name="lastModifiedDate"></param>
        /// <returns>Връща всички ресурси</returns>
        IDictionary<string, string> Search(string lang, out DateTime? lastModifiedDate);

        ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на езиците.
    /// </summary>
    public interface ILanguages : ILoadable
    {
        /// <summary>
        /// Взимане на даден език по код.
        /// </summary>
        /// <param name="code">Код по който да бъде върнат даден език.</param>
        /// <returns>Език</returns>
        Language Get(string code);

        /// <summary>
        /// Връща всички езици от кеша.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Language> Search();

        /// <summary>
        /// Връща всички езици.
        /// </summary>
        IEnumerable<Language> Search(out DateTime? lastModifiedDate);

        /// <summary>
        /// Връша езикът по подразбиране.
        /// </summary>
        /// <returns></returns>
        Language GetDefault();

        Language GetLanguageOrDefault(string code);
        int GetLanguageID(string code);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на декларативни обстоятелства и политики
    /// </summary>
    public interface IDeclarations : ILoadable
    {
        IEnumerable<Declaration> Search();
        IEnumerable<Declaration> Search(IList<int> declarationIDs);
        IEnumerable<Declaration> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на начините на предаване.
    /// </summary>
    public interface IDeliveryChannels : ILoadable
    {
        IEnumerable<DeliveryChannel> Search();
        IEnumerable<DeliveryChannel> Search(IList<short> deliveryChannelIDs);
        IEnumerable<DeliveryChannel> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на видовете документи, които поддържа портала.
    /// </summary>
    public interface IDocumentTypes : ILoadable
    {
        IEnumerable<DocumentType> Search();
        IEnumerable<DocumentType> Search(IList<int> docTypeIDs);
        
        DocumentType this[int documentTypeID] { get; }

        DocumentType GetByDocumentTypeUri(string uri);
        IEnumerable<DocumentType> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на административните услуги.
    /// </summary>
    public interface IServices : ILoadable
    {
        Service Get(string lang, int serviceID);
        IEnumerable<Service> Search();
        IEnumerable<Service> Search(string lang);
        IEnumerable<Service> Search(string lang, int groupID);
        IEnumerable<Service> Search(string lang, out DateTime? lastModifiedDate);
        ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на групи услуги по направление на дейност в МВР
    /// </summary>
    public interface IServiceGroups : ILoadable
    {
        ServiceGroup Get(string lang, int id);
        IEnumerable<ServiceGroup> Search(string lang);
        IEnumerable<ServiceGroup> Search(string lang, out DateTime? lastModifiedDate);
        ValueTask EnsureLoadedAsync(string lang, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на начини на предаване на административните услуги в административните структури.
    /// </summary>
    public interface IServiceTerms : ILoadable
    {
        IEnumerable<ServiceTerm> Search();
        IEnumerable<ServiceTerm> Search(int? serviceID);
        IEnumerable<ServiceTerm> Search(out DateTime? lastModifiedDate);
    }

    public interface IEkatte : ILoadable
    {
        IEnumerable<Ekatte> Search();

        IEnumerable<Ekatte> Search(out DateTime? lastDateModified);
    }

    public interface IGrao : ILoadable
    {
        IEnumerable<Grao> Search();

        IEnumerable<Grao> Search(out DateTime? lastDateModified);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на шаблони за документи.
    /// </summary>
    public interface IDocumentTemplates : ILoadable
    {
        IEnumerable<DocumentTemplate> Search();
        DocumentTemplate Search(int documentTypeID);
        IEnumerable<DocumentTemplate> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на полетата в шаблон за документ.
    /// </summary>
    public interface IDocumentTemplateFields : ILoadable
    {
        IEnumerable<DocumentTemplateField> Search();
        IEnumerable<DocumentTemplateField> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на номеклатура на дъражавите.
    /// </summary>
    public interface ICountries : ILoadable
    {
        IEnumerable<Country> Search();
        IEnumerable<Country> Search(out DateTime? lastModifiedDate);
    }
}
