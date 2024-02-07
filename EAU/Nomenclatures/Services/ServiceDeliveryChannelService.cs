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
    public interface IServiceDeliveryChannelService
    {
        Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<DeliveryChannel> objs, CancellationToken cancellationToken);
        Task<OperationResult> UpdateCollectionAsync(int? serviceID, IEnumerable<DeliveryChannel> objs, CancellationToken cancellationToken);
    }

    internal class ServiceDeliveryChannelService : IServiceDeliveryChannelService
    {
        private readonly IServiceDeliveryChannelRepository _serviceDeliveryChannelRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        public ServiceDeliveryChannelService(
            IServiceDeliveryChannelRepository serviceDeliveryChannelRepository,
            IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _serviceDeliveryChannelRepository = serviceDeliveryChannelRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        public async Task<OperationResult> CreateAsync(int? serviceID, IEnumerable<DeliveryChannel> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                foreach (var dc in objs)
                {
                    await _serviceDeliveryChannelRepository.CreateAsync(
                           new ServiceDeliveryChannel()
                           {
                               DeliveryChannelID = dc.DeliveryChannelID,
                               ServiceID = serviceID
                           });
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);    
        }

        public async Task<OperationResult> UpdateCollectionAsync(int? serviceID, IEnumerable<DeliveryChannel> objs, CancellationToken cancellationToken)
        {
            return await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                var olds = await _serviceDeliveryChannelRepository.SearchAsync(
                new ServiceDeliveryChannelSearchCriteria()
                {
                    ServiceIDs = new int[] { serviceID.Value }
                });

                //create
                var newDocTypes = new List<DeliveryChannel>();
                foreach (var n in objs)
                {
                    if (!olds.Any(o => o.DeliveryChannelID == n.DeliveryChannelID))
                    {
                        newDocTypes.Add(n);
                    }
                }
                await CreateAsync(serviceID, newDocTypes, cancellationToken);

                //delete
                foreach (var o in olds)
                {
                    if (!objs.Any(n => n.DeliveryChannelID == o.DeliveryChannelID))
                    {
                        await _serviceDeliveryChannelRepository.DeleteAsync(o);
                    }
                }

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }
    }
}
