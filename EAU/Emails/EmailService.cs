using CNSys;
using CNSys.Data;
using EAU.Emails.Models;
using EAU.Emails.Repositories;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Emails
{
    /// <summary>
    /// Интерфейс на услуга за работа с имейли.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Създаване на имейли.
        /// </summary>
        /// <param name="email">Имейл съобщение.</param>
        /// <param name="cancellationToken">Билет за отказване на операция.</param>
        /// <returns></returns>
        Task<OperationResult<OperationResultTypes>> CreateEmailAsync(EmailMessage email, CancellationToken cancellationToken);

        /// <summary>
        /// Добавяне на транслитерация на тялото на имейл.
        /// </summary>
        /// <param name="email">Имейл съобщение.</param>
        /// <param name="latinParamName">Име на параметър с който да бъде заменена транслитерацията.</param>
        /// <returns></returns>
        OperationResult AddEmailBodyTransliteration(EmailMessage email, string latinParamName);

        /// <summary>
        /// Изчитане на имейли за изпращане.
        /// </summary>
        /// <param name="maxPendingEmailsCount">Брой имейли в статус "за изпращане", които да бъдат изчетени.</param>
        /// <param name="cancellationToken">Билет за отказване на операция.</param>
        /// <returns>Списък от имейли за изпращане.</returns>
        Task<List<EmailMessage>> GetPendingEmailsAsync(int maxPendingEmailsCount, CancellationToken cancellationToken);

        /// <summary>
        /// Отбелязване на състоянието на имейл след опит за изпращане.
        /// </summary>
        /// <param name="emailID">Идентификатор на имейл съобщение.</param>
        /// <param name="isSend">Флаг указващ дали имейлът да бъде маркиран като успешно или неуспешно изпратен.</param>
        /// <param name="cancellationToken">Билет за отказване на операция.</param>
        /// <returns>Флаг, указващ дали изпращането на съобщението е прекратено поради достигане на максималния брой опити за изпращане</returns>
        Task<OperationResult<bool>> EmailSendAttemptAsync(int emailID, bool isSend, CancellationToken cancellationToken);
    }

    /// <summary>
    ///  Реализация на интерфейс IEmailService за работа с имейли.
    /// </summary>
    internal class EmailService : IEmailService
    {
        #region Private members

        private readonly Regex _extractedBodyRegex = null;
        private IEmailRepository _emailRepository;
        private IDbContextOperationExecutor _dbContextOperationExecutor;

        #endregion

        #region Constructor

        public EmailService(IEmailRepository emailRepository, IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _emailRepository = emailRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
            _extractedBodyRegex = new Regex(@"[\s\S]*<body>([\s\S]*)</body>[\s\S]*");
        }

        #endregion

        #region Public Interface

        public Task<OperationResult<OperationResultTypes>> CreateEmailAsync(EmailMessage email, CancellationToken cancellationToken)
        {
            return _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
            {
                if (email.Recipients == null || email.Recipients.Length == 0)
                    throw new NotSupportedException("no mail addresses");

                await _emailRepository.CreateAsync(email, innerToken);

                if (!email.EmailID.HasValue)
                    throw new NotSupportedException("Email not created");

                var result = new OperationResult<OperationResultTypes>(OperationResultTypes.SuccessfullyCompleted);

                return result;
            }, cancellationToken);
        }

        public OperationResult AddEmailBodyTransliteration(EmailMessage email, string latinParamName)
        {
            var result = new OperationResult(OperationResultTypes.SuccessfullyCompleted);

            if (email.IsBodyHtml)
            {
                var body = new StringBuilder(email.Body);

                string extractedBody = _extractedBodyRegex.Match(body.ToString()).Groups[1].Value;
                extractedBody = extractedBody.Replace(latinParamName, string.Empty);

                string translatedBody = TransliterationHelper.Transliterate(extractedBody);

                body.Replace(latinParamName, translatedBody);

                email.Body = body.ToString();
            }

            return result;
        }

        public Task<List<EmailMessage>> GetPendingEmailsAsync(int maxPendingEmailsCount, CancellationToken cancellationToken)
        {
            return _emailRepository.GetPendingAsync(maxPendingEmailsCount, cancellationToken);
        }

        public Task<OperationResult<bool>> EmailSendAttemptAsync(int emailID, bool isSend, CancellationToken cancellationToken)
        {
            return _dbContextOperationExecutor.ExecuteAsync(async(dbContext, innerToken) =>
            {
                bool isFailedInternal = (await _emailRepository.SendAttemptAsync(emailID, isSend, innerToken));

                var result = new OperationResult<bool>(OperationResultTypes.SuccessfullyCompleted);

                result.Result = isFailedInternal;

                return result;
            }, cancellationToken);
        }

        #endregion
    }
}
