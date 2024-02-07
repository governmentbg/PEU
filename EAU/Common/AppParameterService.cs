using CNSys;
using CNSys.Data;
using EAU.Common.Cache;
using EAU.Common.Repositories;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.ServiceLimits.Cache;
using EAU.ServiceLimits.Models;
using EAU.ServiceLimits.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Common
{
    /// <summary>
    /// Интерфейс за работа с параметри на системата.
    /// </summary>
    public interface IAppParameterService
    {
        /// <summary>
        /// Редакция на стойност на параметър.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="valueDateTime">Дата.</param>
        /// <param name="valueInterval">Интервал.</param>
        /// <param name="valueString">Текст.</param>
        /// <param name="valueInt">Цяло число.</param>
        /// <param name="valueHour">Час.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task<OperationResult> UpdateAsync(string code, DateTime? valueDateTime, TimeSpan? valueInterval,
            string valueString, int? valueInt, TimeSpan? valueHour, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс за работа с параметри на системата.
    /// </summary>
    internal class AppParameterService : IAppParameterService
    {
        private readonly IAppParameterRepository _appParameterRepository;
        private readonly IDbContextOperationExecutor _dBContextOperationExecutor;

        public AppParameterService(
            IAppParameterRepository appParameterRepository,
            IDbContextOperationExecutor dBContextOperationExecutor)
        {
            _appParameterRepository = appParameterRepository;
            _dBContextOperationExecutor = dBContextOperationExecutor;
        }

        public Task<OperationResult> UpdateAsync(string code, DateTime? valueDateTime, TimeSpan? valueInterval,
            string valueString, int? valueInt, TimeSpan? valueHour, CancellationToken cancellationToken)
        {
            return _dBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var itemCol = await _appParameterRepository.SearchInfoAsync(new Models.AppParameterSearchCriteria() { Code = code, CodeIsExact = true }, cancellationToken);
                var item = itemCol.Data.Single();
                item.ValueDateTime = valueDateTime;
                item.ValueInterval = valueInterval;
                item.ValueString = string.IsNullOrEmpty(valueString) && item.ParameterType == Models.AppParameterTypes.String ? string.Empty : valueString;
                item.ValueInt = valueInt;
                item.ValueHour = valueHour;
                await _appParameterRepository.UpdateAsync(item, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
