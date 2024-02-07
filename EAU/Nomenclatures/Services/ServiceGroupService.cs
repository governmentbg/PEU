using CNSys;
using CNSys.Data;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures
{
    public interface IServiceGroupService
    {
        Task<OperationResult> CreateAsync(ServiceGroup obj, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(ServiceGroup obj, CancellationToken cancellationToken);
        Task<OperationResult> CreateAsync(ServiceGroupTranslation obj, string lang, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(ServiceGroupTranslation obj, string lang, CancellationToken cancellationToken);
        Task<OperationResult> StatuChangeAsync(int groupID, Status status, CancellationToken cancellationToken);
    }

    internal class ServiceGroupService : IServiceGroupService
    {
        private readonly IServiceGroupRepository _serviceGroupRepository;
        private readonly IServiceGroupTranslationRepository _translationRepository;
        private readonly IServiceGroups _groups;
        private readonly IServices _services;
        private readonly ILanguages _languages;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        public ServiceGroupService(
            IServiceGroupRepository serviceGroupRepository,
            IServiceGroupTranslationRepository translationRepository,
            IServiceGroups groups,
            IServices services,
            ILanguages languages,
            IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _serviceGroupRepository = serviceGroupRepository;
            _translationRepository = translationRepository;
            _groups = groups;
            _services = services;
            _languages = languages;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        public async Task<OperationResult> CreateAsync(ServiceGroup obj, CancellationToken cancellationToken)
        {
            var result = await ValidateAsync(obj, cancellationToken);

            if (result.IsSuccessfullyCompleted)
            {
                obj.IsActive = false;
                await _serviceGroupRepository.CreateAsync(obj);
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(ServiceGroup obj, CancellationToken cancellationToken)
        {
            var result = await ValidateAsync(obj, cancellationToken);

            if (result.IsSuccessfullyCompleted)
            {
                await _serviceGroupRepository.UpdateAsync(obj);
            }

            return result;
        }

        public async Task<OperationResult> CreateAsync(ServiceGroupTranslation obj, string lang, CancellationToken cancellationToken)
        {
            OperationResult result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (!String.IsNullOrWhiteSpace(obj.Name))
            {
                result = await ValidateNameAsync(obj.GroupID, obj.Name, lang, cancellationToken);
            }

            if (result.IsSuccessfullyCompleted)
            {
                obj.LanguageID = _languages.GetLanguageID(lang);
                await _translationRepository.CreateAsync(obj, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> UpdateAsync(ServiceGroupTranslation obj, string lang, CancellationToken cancellationToken)
        {
            OperationResult result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (!String.IsNullOrWhiteSpace(obj.Name))
            {
                result = await ValidateNameAsync(obj.GroupID, obj.Name, lang, cancellationToken);
            }

            if (result.IsSuccessfullyCompleted)
            {
                obj.LanguageID = _languages.GetLanguageID(lang);
                await _translationRepository.UpdateAsync(obj, cancellationToken);
            }

            return result;
        }

        public async Task<OperationResult> StatuChangeAsync(int groupID, Status status, CancellationToken cancellationToken)
        {
            var result = ValidateStatusChange(groupID, status);
            if (result.IsSuccessfullyCompleted)
            {
                var group = _groups.Get(_languages.GetDefault().Code, groupID);
                group.IsActive = (status == Status.Activate ? true : false);

                await _serviceGroupRepository.UpdateAsync(group);
            }

            return result;
        }

        private async Task<OperationResult> ValidateAsync(ServiceGroup obj, CancellationToken cancellationToken)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (String.IsNullOrWhiteSpace(obj.Name))
                //Полето \"Наименование\" трябва да бъде попълнено. 
                result.AddError(new TextError("SERVICE_GROUP_NAME", "SERVICE_GROUP_NAME"), true);

            if (result.IsSuccessfullyCompleted)
            {
                result.Merge(await ValidateNameAsync(obj.GroupID, obj.Name, _languages.GetDefault().Code, cancellationToken));
            }

            if (obj.OrderNumber == null)
                //Полето \"Пореден номер\" трябва да бъде попълнено. 
                result.AddError(new TextError("GL_MANDATORY_NUMBER_IN_GROUP_E", "GL_MANDATORY_NUMBER_IN_GROUP_E"), true);

            //ТОДО Здравко -> За какво се ползва и трябва ли да е задължително ?!
            //if (String.IsNullOrWhiteSpace(obj.CssClassStr))
            //    //Добавянето на снимка към групата услуги е задължително. 
            //    result.AddError(new TextError("SERVICE_GROUP_CSSCLASSSTR", "SERVICE_GROUP_CSSCLASSSTR"), true);

            return result;
        }

        private async Task<OperationResult> ValidateNameAsync(int? groupID, string name, string lang, CancellationToken cancellationToken)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            await _groups.EnsureLoadedAsync(lang, cancellationToken);
            foreach (var g in _groups.Search(lang))
            {
                if (groupID != g.GroupID &&
                    g.Name.ToLower().Trim() == name.ToLower().Trim())
                {
                    //Има създадена услуга със същото наименование. 
                    result.AddError(new TextError("SERVICE_GROUP_NAME", "SERVICE_GROUP_NAME"), true);
                }
            }

            return result;
        }


        private OperationResult ValidateStatusChange(int groupID, Status status)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (status == Status.Deactivate)
            {
                if (_services.Search(_languages.GetDefault().Code, groupID).Where(x => x.IsActive.GetValueOrDefault()).Any())
                {
                    //Групата услуги може да бъде деактивирана, само ако всички услуги, който са включени в нея бъдат деактивирани.
                    result.AddError(new TextError("GL_SERVICE_GROUP_CANNOT_BE_DEACTIVATE_E", "GL_SERVICE_GROUP_CANNOT_BE_DEACTIVATE_E"), true);
                }
            }

            return result;
        }
    }
}
