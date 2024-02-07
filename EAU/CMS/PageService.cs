using CNSys;
using CNSys.Data;
using EAU.CMS.Models;
using EAU.CMS.Repositories;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.CMS
{
    /// <summary>
    /// Интерфейс за работа със страници с html съдържание.
    /// </summary>
    public interface IPageService
    {
        /// <summary>
        /// Операция за редакция на страница.
        /// </summary>
        /// <param name="pageID">Идентификатор на страница</param>
        /// <param name="title">Заглавие на страница</param>
        /// <param name="content">Съдържание на страница</param>
        /// <param name="cancellationToken">Разпространява известие, че операциите трябва да бъдат отменени.</param>
        Task<OperationResult> UpdateAsync(int pageID, string title, string content, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс за работа със страници с html съдържание.
    /// </summary>
    internal class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        public PageService(
            IPageRepository pageRepository,
            IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _pageRepository = pageRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        public Task<OperationResult> UpdateAsync(int pageID, string title, string content, CancellationToken cancellationToken)
        {
            return _dbContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var itemCol = await _pageRepository.SearchInfoAsync(new PageSearchCriteria() { PageIDs = new int[] { pageID } }, cancellationToken);
                var item = itemCol.Data.Single();
                item.Title = title;
                item.Content = content;
                await _pageRepository.UpdateAsync(item, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
