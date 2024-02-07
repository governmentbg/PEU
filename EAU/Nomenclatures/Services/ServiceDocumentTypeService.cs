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
    public interface IServiceDocumentTypeService
    {
        Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<DocumentType> objs, CancellationToken cancellationToken);
        Task<OperationResult> UpdateCollectionAsync(int? serviceID, IEnumerable<DocumentType> objs, CancellationToken cancellationToken);
    }

    internal class ServiceDocumentTypeService : IServiceDocumentTypeService
    {
        private readonly IServiceDocumentTypeRepository _serviceDocumentTypeRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        public ServiceDocumentTypeService(
            IServiceDocumentTypeRepository serviceDocumentTypeRepository,
            IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _serviceDocumentTypeRepository = serviceDocumentTypeRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        public async Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<DocumentType> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                foreach (var adt in objs)
                {
                    await _serviceDocumentTypeRepository.CreateAsync(
                        new ServiceDocumentType()
                        {
                            DocTypeID = adt.DocumentTypeID,
                            ServiceID = serviceID
                        });
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);

        }

        public async Task<OperationResult> UpdateCollectionAsync(int? serviceID, IEnumerable<DocumentType> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {

                var olds = await _serviceDocumentTypeRepository.SearchAsync(
                new ServiceDocumentTypeSearchCriteria()
                {
                    ServiceIDs = new int[] { serviceID.Value }
                });

                //TODO START TRANSACTION
                //create
                var newServiceDocs = new List<DocumentType>();
                foreach (var n in objs)
                {
                    if (!olds.Any(o => o.DocTypeID == n.DocumentTypeID))
                    {
                        newServiceDocs.Add(n);
                    }
                }
                await CreateAsync(serviceID, newServiceDocs, cancellationToken);

                //delete
                foreach (var o in olds)
                {
                    if (!objs.Any(n => n.DocumentTypeID == o.DocTypeID))
                    {
                        await _serviceDocumentTypeRepository.DeleteAsync(o);
                    }
                }


                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
