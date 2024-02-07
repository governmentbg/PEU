using CNSys.Data;
using EAU.Data;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип Signer.
    /// </summary>
    public interface ISignersRepository : IRepositoryAsync<Signer, long?, SignerSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс ISignersEntity за поддържане и съхранение на обекти от тип Signer.
    /// </summary>
    internal class SignersRepository : EAURepositoryBase<Signer, long?, SignerSearchCriteria, SigningDataContext>, ISignersRepository
    {
        #region Constructors

        public SignersRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected async override Task CreateInternalAsync(SigningDataContext context, Signer item, CancellationToken cancellationToken)
        {
            long? signerID = await context.SignerCreateAsync(item.ProcessID, item.Name, item.Ident, item.Order, cancellationToken);

            item.SignerID = signerID;
        }

        protected override Task UpdateInternalAsync(SigningDataContext context, Signer item, CancellationToken cancellationToken)
        {
            var status = item.Status.HasValue ? (short?)item.Status.Value : null;
            var channel = item.SigningChannel.HasValue ? (short?)item.SigningChannel.Value : null;

            return context.SignerUpdateAsync(item.SignerID, status, channel, item.SigningAdditionalData, item.TransactionID, item.RejectReson, cancellationToken);
        }

        protected override Task DeleteInternalAsync(SigningDataContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new System.ArgumentNullException();

            return context.SignerDeleteAsync(key, cancellationToken);
        }

        protected override Task DeleteInternalAsync(SigningDataContext context, Signer item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.SignerID, cancellationToken);
        }

        protected async override Task<Signer> ReadInternalAsync(SigningDataContext context, long? key, CancellationToken cancellationToken)
        {
            return (await SearchInternalAsync(context, new SignerSearchCriteria() { SignerID = key }  , cancellationToken)).SingleOrDefault();
        }

        protected async override Task<IEnumerable<Signer>> SearchInternalAsync(SigningDataContext context, PagedDataState state, SignerSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            List<Signer> signers = null;
            var channel = searchCriteria.SigningChannel.HasValue ? (short?)searchCriteria.SigningChannel.Value : null;

            var res = await context.SignersSearchAsync(
                    searchCriteria.SignerID,
                    searchCriteria.ProcessIDs,
                    channel,
                    searchCriteria.TransactionID,
                    state.StartIndex,
                    state.PageSize,
                    state.StartIndex == 1,
                    cancellationToken);

            state.Count = res.count ?? state.Count;
            signers = res.signers.ToList();

            return signers;
        }

        protected override Task<IEnumerable<Signer>> SearchInternalAsync(SigningDataContext context, SignerSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        #endregion
    }
}
