using CNSys.Data;
using EAU.Common;
using EAU.Data;
using EAU.Signing.Models;
using EAU.Signing.Models.SearchCriteria;
using Microsoft.Extensions.Options;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип SigningProcess.
    /// </summary>
    public interface ISigningProcessesRepository : IRepositoryAsync<SigningProcess, Guid?, SigningProcessesSearchCriteria>
    {
        /// <summary>
        /// Качва съдържание
        /// </summary>
        /// <param name="content">съдържание</param>
        /// <param name="signingProcessID">уникален идентификатор на процес по подписване</param>
        /// <param name="cancellationToken"></param>
        Task UploadContentAsync(Stream content, Guid signingProcessID, CancellationToken cancellationToken);

        /// <summary>
        /// Изтрива процеси по подписване.
        /// </summary>
        /// <param name="guids">Идентификатори на процеси по подписване.</param>
        /// <param name="cancellationToken"></param>
        Task DeleteSigningProcessesAsync(Guid[] guids, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс ISigningProcessesEntity за поддържане и съхранение на обекти от тип SigningProcess.
    /// </summary>
    internal class SigningProcessesRepository : EAURepositoryBase<SigningProcess, Guid?, SigningProcessesSearchCriteria, SigningDataContext>, ISigningProcessesRepository
    {
        private readonly GlobalOptions _globalOptions;
        private readonly ISignersRepository _signersRepository;

        #region Constructors

        public SigningProcessesRepository(IOptionsMonitor<GlobalOptions> optionsAccessor, ISignersRepository signersRepository, IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
            _globalOptions = optionsAccessor.CurrentValue;
            _signersRepository = signersRepository;
        }

        #endregion

        #region ISigningProcessesEntity

        public async Task UploadContentAsync(Stream content, Guid signingProcessID, CancellationToken cancellationToken)
        {
            await ClearContent(signingProcessID, cancellationToken);
            await UploadContentInternalAsync(content, signingProcessID, cancellationToken);
        }

        public Task DeleteSigningProcessesAsync(Guid[] guids, CancellationToken cancellationToken)
        {
            return DoOperationAsync((context, token) =>
            {
                return context.SigningProcessesDeleteAllAsync(guids, token);
            }, cancellationToken);
        }

        #endregion

        #region CRUD

        protected async override Task CreateInternalAsync(SigningDataContext context, SigningProcess item, CancellationToken cancellationToken)
        {
            var format = item.Format.HasValue ? (short?)item.Format.Value : null;
            var level = item.Level.HasValue ? (short?)item.Level.Value : null;
            var type = item.Type.HasValue ? (short?)item.Type.Value : null;
            var digestMethod = item.DigestMethod.HasValue ? (short?)item.DigestMethod.Value : null;

            if (!item.ProcessID.HasValue)
            {
                item.ProcessID = Guid.NewGuid();
            }

            await context.SigningProcessCreateAsync(item.ProcessID,
                format,
                item.FileName,
                item.ContentType,
                level,
                type,
                digestMethod,
                item.RejectedCallbackUrl,
                item.CompletedCallbackUrl,
                item.AdditionalData,
                cancellationToken);

            if(item.Content != null)
            {
                await UploadContentInternalAsync(item.Content, item.ProcessID.Value, cancellationToken);
            }
        }

        protected override Task UpdateInternalAsync(SigningDataContext context, SigningProcess item, CancellationToken cancellationToken)
        {
            short? status = item.Status.HasValue ? (short?)item.Status.Value : null;
            return context.SigningProcessUpdateAsync(item.ProcessID, status, item.AdditionalData, cancellationToken);
        }

        protected override Task DeleteInternalAsync(SigningDataContext context, Guid? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            return context.SigningProcessDeleteAsync(key, cancellationToken);
        }

        protected override Task DeleteInternalAsync(SigningDataContext context, SigningProcess item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.ProcessID, cancellationToken);
        }

        protected async override Task<SigningProcess> ReadInternalAsync(SigningDataContext context, Guid? key, CancellationToken cancellationToken)
        {
            return (await SearchAsync(new SigningProcessesSearchCriteria() { ProcessID = key }, cancellationToken)).SingleOrDefault();
        }

        protected async override Task<IEnumerable<SigningProcess>> SearchInternalAsync(SigningDataContext context, PagedDataState state, SigningProcessesSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            List<SigningProcess> result = null;
            var status = searchCriteria.Status.HasValue ? (short?)searchCriteria.Status.Value : null;

            var (processes, count) = await context.SigningProcessSearchAsync(
                   searchCriteria.ProcessID,
                   status,
                   searchCriteria.WithLock,
                   state.StartIndex,
                   state.PageSize,
                   state.StartIndex == 1,
                   cancellationToken);

            state.Count = count ?? state.Count;
            result = processes.ToList();

            if (result != null && result.Count > 0)
            {
                if (searchCriteria.LoadContent)
                {
                    result.ForEach(el => { el.Content = SigningDataContext.CreateSigningProcessContenStream(el.ProcessID.Value, DbContextProvider); });
                }

                if (searchCriteria.LoadSigners)
                {
                    var processIDs = result.Select(el => el.ProcessID.Value).ToList();

                    var signers = await _signersRepository.SearchAsync(new SignerSearchCriteria() { ProcessIDs = processIDs });

                    result.ForEach(el => { el.Signers = signers.Where(s => s.ProcessID == el.ProcessID).ToList(); });
                }
            }

            return result;
        }

        protected override Task<IEnumerable<SigningProcess>> SearchInternalAsync(SigningDataContext context, SigningProcessesSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        #endregion

        private Task ClearContent(Guid signingProcessID, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (ctx, token) =>
            {
                await ctx.SigningProcessContentUploadAsync(signingProcessID, null, 0, token);
            }, cancellationToken);
        }

        private Task UploadContentInternalAsync(Stream content, Guid signingProcessID, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (ctx, token) =>
            {
                if (content.Position != 0)
                    throw new ArgumentException("The content stream must be in 0 position in order to be uploaded.");

                int chunkSize = _globalOptions.GL_COPY_BUFFER_SIZE;
                byte[] buffer = null;

                try
                {
                    buffer = ArrayPool<byte>.Shared.Rent(chunkSize);
                    int bytesReaded = 0;

                    while ((bytesReaded = content.Read(buffer, 0, chunkSize)) > 0)
                    {
                        await ctx.SigningProcessContentUploadAsync(signingProcessID, buffer, bytesReaded, token);
                    }
                }
                finally
                {
                    if (buffer != null)
                        ArrayPool<byte>.Shared.Return(buffer);
                }
            }, cancellationToken);
        }
    }
}
