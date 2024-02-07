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

namespace EAU.Nomenclatures.Services
{
    public interface IServiceDeclarationService
    {
        Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<Declaration> objs, CancellationToken cancellationToken);
        Task<OperationResult> UpdateCollectionAsync(int? serviceID, IEnumerable<Declaration> objs, CancellationToken cancellationToken);
    }

    internal class ServiceDeclarationService : IServiceDeclarationService
    {
        private readonly IServiceDeclarationRepository _serviceDeclarationRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        public ServiceDeclarationService(
            IServiceDeclarationRepository serviceDeclarationRepository,
            IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _serviceDeclarationRepository = serviceDeclarationRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        public async Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<Declaration> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                foreach (var d in objs)
                {
                    await _serviceDeclarationRepository.CreateAsync(
                             new ServiceDeclaration()
                             {
                                 DeclarationID = d.DeclarationID,
                                 ServiceID = serviceID
                             });
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task<OperationResult> UpdateCollectionAsync(int? serviceID, IEnumerable<Declaration> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                var olds = await _serviceDeclarationRepository.SearchAsync(
                new ServiceDeclarationSearchCriteria()
                {
                    ServiceIDs = new int[] { serviceID.Value }
                });

                //create
                var newDocTypes = new List<Declaration>();
                foreach (var n in objs)
                {
                    if (!olds.Any(o => o.DeclarationID == n.DeclarationID))
                    {
                        newDocTypes.Add(n);
                    }
                }
                await CreateAsync(serviceID, newDocTypes, cancellationToken);

                //delete
                foreach (var o in olds)
                {
                    if (!objs.Any(n => n.DeclarationID == o.DeclarationID))
                    {
                        await _serviceDeclarationRepository.DeleteAsync(o);
                    }
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
