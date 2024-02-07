using CNSys;
using CNSys.Data;
using EAU.Nomenclatures.Cache;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.Nomenclatures.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EAU.Nomenclatures.Services
{
    public interface IServiceService
    {
        Task<OperationResult> CreateAsync(Service obj, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(Service obj, CancellationToken cancellationToken);
        Task<OperationResult> CreateAsync(ServiceTranslation obj, string lang, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(ServiceTranslation obj, string lang, CancellationToken cancellationToken);
        Task<OperationResult> StatuChangeAsync(int serviceID, Status obj, CancellationToken cancellationToken);
        Task<CollectionInfo<Service>> SearchInfoAsync(ServiceSearchCriteria searchCriteria);
        Task<CollectionInfo<Service>> SearchInfoAsync(ServiceSearchCriteria searchCriteria, CancellationToken cancellationToken);
    }

    internal class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceTranslationRepository _translationRepository;
        private readonly IServiceDeclarationRepository _serviceDeclarationRepository;
        private readonly IServiceTermRepository _serviceTermRepository;
        private readonly IServiceDeliveryChannelRepository _serviceDeliveryChannelRepository;
        private readonly IServiceDocumentTypeRepository _serviceDocumentTypeRepository;

        private readonly IServiceDeclarationService _serviceDeclarationService;
        private readonly IServiceTermService _serviceTermService;
        private readonly IServiceDeliveryChannelService _serviceDeliveryChannelService;
        private readonly IServiceDocumentTypeService _serviceDocumentTypeService;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        private readonly IServiceTerms _serviceTerms;
        private readonly IDocumentTypes _documentTypes;
        private readonly IDeclarations _declarations;
        private readonly IDeliveryChannels _deliveryChannels;
        private readonly ILanguages _languages;

        public ServiceService(
            IServiceRepository serviceRepository,
            IServiceTranslationRepository translationRepository,
            IServiceDeclarationRepository serviceDeclarationRepository,
            IServiceTermRepository serviceTermRepository,
            IServiceDeliveryChannelRepository serviceDeliveryChannelRepository,
            IServiceDocumentTypeRepository serviceDocumentTypeRepository,
            IServiceDeclarationService serviceDeclarationService,
            IServiceTermService serviceTermService,
            IServiceDeliveryChannelService serviceDeliveryChannelService,
            IServiceDocumentTypeService serviceDocumentTypeService,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IServiceTerms serviceTerms,
            IDocumentTypes documentTypes,
            IDeclarations declarations,
            IDeliveryChannels deliveryChannels,
            ILanguages languages)
        {
            _serviceRepository = serviceRepository;
            _translationRepository = translationRepository;
            _serviceDeclarationRepository = serviceDeclarationRepository;
            _serviceTermRepository = serviceTermRepository;
            _serviceDeliveryChannelRepository = serviceDeliveryChannelRepository;
            _serviceDocumentTypeRepository = serviceDocumentTypeRepository;
            _serviceDeclarationService = serviceDeclarationService;
            _serviceTermService = serviceTermService;
            _serviceDeliveryChannelService = serviceDeliveryChannelService;
            _serviceDocumentTypeService = serviceDocumentTypeService;
            _dbContextOperationExecutor = dbContextOperationExecutor;
            _serviceTerms = serviceTerms;
            _documentTypes = documentTypes;
            _declarations = declarations;
            _deliveryChannels = deliveryChannels;
            _languages = languages;
        }

        public async Task<OperationResult> CreateAsync(Service obj, CancellationToken cancellationToken)
        {
            var result = await Validate(obj);
            if (result.IsSuccessfullyCompleted)
            {
                return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
                {
                    if (!obj.IsActive.HasValue)
                        obj.IsActive = false;

                    await _serviceRepository.CreateAsync(obj);
                    await _serviceDocumentTypeService.CreateAsync(obj.ServiceID, obj.AttachedDocumentTypes, cancellationToken);
                    await _serviceDeclarationService.CreateAsync(obj.ServiceID, obj.Declarations, cancellationToken);
                    await _serviceDeliveryChannelService.CreateAsync(obj.ServiceID, obj.DeliveryChannels, cancellationToken);
                    await _serviceTermService.CreateAsync(obj.ServiceID, obj.SeviceTerms, cancellationToken);

                    return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
                }, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(Service obj, CancellationToken cancellationToken)
        {
            var result = await Validate(obj);
            if (result.IsSuccessfullyCompleted)
            {

                return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
                {
                    await _serviceRepository.UpdateAsync(obj);
                    await _serviceDocumentTypeService.UpdateCollectionAsync(obj.ServiceID, obj.AttachedDocumentTypes, cancellationToken);
                    await _serviceDeclarationService.UpdateCollectionAsync(obj.ServiceID, obj.Declarations, cancellationToken);
                    await _serviceDeliveryChannelService.UpdateCollectionAsync(obj.ServiceID, obj.DeliveryChannels, cancellationToken);
                    await _serviceTermService.UpdateAsync(obj.ServiceID, obj.SeviceTerms, cancellationToken);

                    return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
                }, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> CreateAsync(ServiceTranslation obj, string lang, CancellationToken cancellationToken)
        {
            OperationResult result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (!String.IsNullOrWhiteSpace(obj.Name))
            {
                result = await ValidateName(obj.ServiceID, obj.Name, lang);
            }
            
            if (result.IsSuccessfullyCompleted)
            {
                obj.LanguageID = _languages.GetLanguageID(lang);
                await _translationRepository.CreateAsync(obj, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(ServiceTranslation obj, string lang, CancellationToken cancellationToken)
        {
            OperationResult result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (!String.IsNullOrWhiteSpace(obj.Name))
            {
                result = await ValidateName(obj.ServiceID, obj.Name, lang);
            }

            if (result.IsSuccessfullyCompleted)
            {
                obj.LanguageID = _languages.GetLanguageID(lang);
                await _translationRepository.UpdateAsync(obj, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> StatuChangeAsync(int serviceID, Status status, CancellationToken cancellationToken)
        {
            var obj = (await _serviceRepository.SearchAsync(
                new ServiceSearchCriteria()
                {
                    IDs = new int[] { serviceID }
                })).Single();
            obj.IsActive = (status == Status.Activate ? true : false);

            await _serviceRepository.UpdateAsync(obj);

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public Task<CollectionInfo<Service>> SearchInfoAsync(ServiceSearchCriteria searchCriteria)
        {
            return SearchInfoAsync(searchCriteria, CancellationToken.None);
        }

        public async Task<CollectionInfo<Service>> SearchInfoAsync(ServiceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.SearchInfoAsync(searchCriteria, cancellationToken);

            var serviceIDs = services.Data.Select(x => x.ServiceID.Value).ToList();


            if (serviceIDs.Any())
            {
                var serviceDocuments = await _serviceDocumentTypeRepository.SearchAsync(
                   new ServiceDocumentTypeSearchCriteria()
                   {
                       ServiceIDs = serviceIDs.ToArray()
                   });

                var serviceDeclarations = await _serviceDeclarationRepository.SearchAsync(
                    new ServiceDeclarationSearchCriteria()
                    {
                        ServiceIDs = serviceIDs.ToArray()
                    });


                var serviceDelivaryChannels = await _serviceDeliveryChannelRepository.SearchAsync(
                    new ServiceDeliveryChannelSearchCriteria()
                    {
                        ServiceIDs = serviceIDs.ToArray()
                    });

                //Метода се използва за зареждане на номенклатурата на услугите и се извиква паралено с еншура на другите номенклатури, но трябва да се изчака тяхното зареждане преди да се ползват
                await _documentTypes.EnsureLoadedAsync(cancellationToken);
                await _declarations.EnsureLoadedAsync(cancellationToken);
                await _deliveryChannels.EnsureLoadedAsync(cancellationToken);
                await _serviceTerms.EnsureLoadedAsync(cancellationToken);

                foreach (var service in services.Data)
                {
                    var docIDs = serviceDocuments.Where(sd => sd.ServiceID == service.ServiceID).Select(sd => sd.DocTypeID.Value).ToList();
                    service.AttachedDocumentTypes = _documentTypes.Search(docIDs).ToList();

                    var decIDs = serviceDeclarations.Where(sd => sd.ServiceID == service.ServiceID).Select(sd => sd.DeclarationID.Value).ToList();
                    service.Declarations = _declarations.Search(decIDs).ToList();

                    var delivaryIDs = serviceDelivaryChannels.Where(sd => sd.ServiceID == service.ServiceID).Select(sd => sd.DeliveryChannelID.Value).ToList();
                    service.DeliveryChannels = _deliveryChannels.Search(delivaryIDs).ToList();

                    service.SeviceTerms = _serviceTerms.Search(service.ServiceID).ToList();
                }
            }

            return services;
        }

        private async Task<OperationResult> Validate(Service obj)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (String.IsNullOrWhiteSpace(obj.Name))
                //Полето \"Наименование\" трябва да бъде попълнено.
                result.AddError(new TextError("SERVICE_NAME", "SERVICE_NAME"), true);

            if (result.IsSuccessfullyCompleted)
                result.Merge(await ValidateName(obj.ServiceID, obj.Name, _languages.GetDefault().Code));

            //TODO В полето "Наименование" може да се съдържат само букви на кирилица и символите интервал, ` (апостроф) и - (тире) и , (запетая).

            if (String.IsNullOrWhiteSpace(obj.SunauServiceUri))
                //Полето \"УРИ на административна услуга\" трябва да бъде попълнено.
                result.AddError(new TextError("SERVICE_SUNAUSERVICEURI", "SERVICE_SUNAUSERVICEURI"), true);

            if (obj.InitiationTypeID == null)
                //Полето \"Начин на иницииране на услугата\" трябва да бъде попълнено.
                result.AddError(new TextError("SERVICE_INITIATIONTYPEID", "SERVICE_INITIATIONTYPEID"), true);

            if (obj.GroupID == null)
                //Полето \"Група\" трябва да бъде попълнено.
                result.AddError(new TextError("SERVICE_GROUPID", "SERVICE_GROUPID"), true);

            if (obj.OrderNumber == null)
                //Полето \"Пореден номер\" трябва да бъде попълнено.
                result.AddError(new TextError("SERVICE_ORDERNUMBER", "SERVICE_ORDERNUMBER"), true);

            if (obj.InitiationTypeID == WaysToStartService.ByAplication)
            {
                //Полето "Заявление иницииращо услугата" трябва да бъде попълнено.

                if (String.IsNullOrWhiteSpace(obj.AdmStructureUnitName))
                    //Полето \"Наименование на структурна единица\" трябва да бъде попълнено.
                    result.AddError(new TextError("SERVICE_ADMSTRUCTUREUNITNAME", "SERVICE_ADMSTRUCTUREUNITNAME"), true);

                // Начин на получаване 
                if (obj.DeliveryChannels?.Any() == false)
                    //Изберете поле една от възможностите за \"Начин на получаване\".
                    result.AddError(new TextError("SERVICE_DELIVERYCHANNELS", "SERVICE_DELIVERYCHANNELS"), true);
            }
            else if (obj.InitiationTypeID == WaysToStartService.ByRedirectToWebPage)
            {
                if (String.IsNullOrWhiteSpace(obj.ServiceUrl))
                    //Полето "Адрес на услугата" трябва да бъде попълнено. 
                    result.AddError(new TextError("SERVICE_WEBSERVICEURI", "SERVICE_WEBSERVICEURI"), true);
            }

            return result;
        }

        private async Task<OperationResult> ValidateName(int? serviceID, string name, string lang)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            var services = (await _serviceRepository.SearchAsync(
                new ServiceSearchCriteria()
                {
                    Name = name
                }));

            foreach (var s in services)
            {
                if (serviceID != s.ServiceID &&
                    s.Name.ToLower().Trim() == name.ToLower().Trim())
                {
                    //Има създадена група със същото наименование. 
                    result.AddError(new TextError("SERVICE_NAME", "SERVICE_NAME"), true);
                }
            }

            return result;
        }
    }
}
