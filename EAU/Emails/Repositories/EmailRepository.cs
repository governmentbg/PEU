using CNSys.Data;
using EAU.Data;
using EAU.Emails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Emails.Repositories
{
    /// <summary>
    /// Критерии за търсене за работа с имейли.
    /// </summary>
    public class EmailSearchCriteria
    {
        /// <summary>
        /// Статус.
        /// </summary>
        public EmailStatues Status { get; set; }

        /// <summary>
        ///  Приоритет.
        /// </summary>
        public EmailPriority Priority { get; set; }

        /// <summary>
        /// Флаг за разграничаване на съобщенията, за които датата за обработка е (или не е) настъпила.
        /// </summary>
        public bool? IsDoNotProcessBeforeExpired { get; set; }
    }

    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип EmailMessage.
    /// </summary>
    public interface IEmailRepository : 
        IRepository<EmailMessage, long?, EmailSearchCriteria>, 
        IRepositoryAsync<EmailMessage, long?, EmailSearchCriteria>
    {
        /// <summary>
        /// отбелязване на състоянието на имейл след опит за изпращане.      
        /// </summary>
        /// <param name="emailID">Идентификатор на съобщение.</param>
        /// <param name="isSend">Флаг, указващ дали съобщението е изпратено успешно.</param>
        /// <param name="cancellationToken"></param>
        Task<bool> SendAttemptAsync(int emailID, bool isSend, CancellationToken cancellationToken);

        /// <summary>
        /// Връща чакащите емайли за изпращане.
        /// </summary>
        /// <param name="maxFetched"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<EmailMessage>> GetPendingAsync(int maxFetched, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс IEmailEntity за поддържане и съхранение на обекти от тип EmailMessage.
    /// </summary>
    internal class EmailRepository : EAURepositoryBase<EmailMessage, long?, EmailSearchCriteria, EmailDataContext>, IEmailRepository
    {
        #region Constructors

        public EmailRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IEmailEntity

        protected override async Task CreateInternalAsync(EmailDataContext context, EmailMessage item, CancellationToken cancellationToken)
        {
            string recipientsJSON = item.Recipients != null ? System.Text.Json.JsonSerializer.Serialize(item.Recipients) : null;

            var emailID = await context.EmailCreateAsync((short?)item.Priority,
                                item.TryCount,
                                item.Subject,
                                item.Body,
                                item.IsBodyHtml,
                                item.SendingProviderName,
                                recipientsJSON,
                                cancellationToken);

            item.EmailID = emailID;
        }

        protected override async Task<IEnumerable<EmailMessage>> SearchInternalAsync(EmailDataContext dataContext, PagedDataState state, EmailSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var res = await dataContext.EmailSearchAsync((short?)searchCriteria.Priority,
                                                    (short?)searchCriteria.Status,
                                                    searchCriteria.IsDoNotProcessBeforeExpired,
                                                    state.StartIndex,
                                                    state.PageSize,
                                                    (state.StartIndex == 1),
                                                    cancellationToken);

            state.Count = res.count ?? state.Count;
            List<EmailMessage> emailMessages = res.messages.ToList();

            PrepareEmailsRecipients(emailMessages);

            return emailMessages;
        }

        protected override Task<IEnumerable<EmailMessage>> SearchInternalAsync(EmailDataContext context, EmailSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public async Task<bool> SendAttemptAsync(int emailID, bool isSend, CancellationToken cancellationToken)
        {
            if (IsReadOnly)
                throw new NotSupportedException("The entity is read-only!");

            bool? isFailedInt = null;
            await DoOperationAsync(async (dc, innerToken) =>
            {
                isFailedInt = await dc.EmailSendAttemptAsync(emailID,
                                isSend,
                                innerToken);

            }, cancellationToken);

            if (!isFailedInt.HasValue)
                throw new NotSupportedException("isFailedInt must have a value");

            var isFailed = isFailedInt.Value;
            return isFailed;
        }

        public Task<List<EmailMessage>> GetPendingAsync(int maxFetched, CancellationToken cancellationToken)
        {
            if (IsReadOnly)
                throw new NotSupportedException("The entity is read-only!");

            return DoOperationAsync(async (dc, innerToken) =>
            {
                var emailMessages = (await dc.GetPendingAsync(maxFetched, innerToken)).ToList();
                PrepareEmailsRecipients(emailMessages);

                return emailMessages;
            }, cancellationToken);
        }

        #endregion

        private void PrepareEmailsRecipients(IEnumerable<EmailMessage> emailMessages)
        {
            foreach (var email in emailMessages.Where(e => !string.IsNullOrEmpty(e.RecipientsJSONSerialized)))
            {
                email.Recipients = System.Text.Json.JsonSerializer.Deserialize<EmailRecipient[]>(email.RecipientsJSONSerialized);
            }
        }
    }
}
