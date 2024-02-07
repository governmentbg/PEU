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
    public interface IServiceTermService
    {
        Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<ServiceTerm> objs, CancellationToken cancellationToken);
        Task<OperationResult> UpdateAsync(int? serviceID, IEnumerable<ServiceTerm> objs, CancellationToken cancellationToken);
    }

    internal class ServiceTermService : IServiceTermService
    {
        private readonly IServiceTermRepository _serviceTermRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;
        private readonly IServiceTerms _serviceTerms;

        public ServiceTermService(
            IServiceTermRepository serviceTermRepository,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IServiceTerms serviceTerms)
        {
            _serviceTermRepository = serviceTermRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
            _serviceTerms = serviceTerms;
        }

        public async Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<ServiceTerm> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                foreach (var st in objs)
                {
                    st.ServiceID = serviceID;
                    await _serviceTermRepository.CreateAsync(st);
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public async Task<OperationResult> UpdateAsync(int? serviceID, IEnumerable<ServiceTerm> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                
                var olds = _serviceTerms.Search(serviceID.Value);

                //create
                var newDocTypes = new List<ServiceTerm>();
                foreach (var n in objs)
                {
                    if (!olds.Any(o => o.ServiceTermID == n.ServiceTermID))
                    {
                        newDocTypes.Add(n);
                    }
                }

                if (newDocTypes.Any())
                    await CreateAsync(serviceID, newDocTypes, cancellationToken);


                //update
                foreach (var n in objs)
                {
                    var old = olds.SingleOrDefault(o => o.ServiceTermID == n.ServiceTermID);
                    if (old != null &&
                       (old.ServiceTermType != n.ServiceTermType ||
                       old.Price != n.Price ||
                       old.ExecutionPeriod != n.ExecutionPeriod ||
                       old.Description != n.Description ||
                       old.PeriodType != n.PeriodType ||
                       old.IsActive != n.IsActive))
                    {
                        await _serviceTermRepository.UpdateAsync(n);
                    }
                }

                //delete
                foreach (var o in olds)
                {
                    if (!objs.Any(n => n.ServiceTermID == o.ServiceTermID))
                    {
                        await _serviceTermRepository.DeleteAsync(o);
                    }
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
