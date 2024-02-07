using CNSys;
using CNSys.Data;
using EAU.Emails.Cache;
using EAU.Emails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Emails
{
    /// <summary>
    /// Интерфейс на услуга за работа с имейли.
    /// </summary>
    public interface IEmailNotificationService
    {
        /// <summary>
        /// Създаване на имейл нотификация.
        /// </summary>
        /// <param name="request">Заявка за създаване на имейл нотификация.</param>
        /// <returns></returns>
        Task<OperationResult<EmailNotificationResponse>> CreateEmailNotificationAsync(EmailNotificationRequest request);
    }

    /// <summary>
    ///  Реализация на интерфейс IEmailNotificationService за работа с имейли.
    /// </summary>
    internal class EmailNotificationService : IEmailNotificationService
    {
        public const string PARAM_TRANSLITERATION_NAME = "paramTransliteration";

        #region Private members

        private readonly IEmailService _emailService;
        private readonly IEmailsCache _emailsCache;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;

        #endregion

        #region Constructor

        public EmailNotificationService(IEmailService emailService, IEmailsCache emailsCache, IDbContextOperationExecutor dbContextOperationExecutor)
        {
            _emailService = emailService;
            _emailsCache = emailsCache;
            _dbContextOperationExecutor = dbContextOperationExecutor;
        }

        #endregion

        #region Public Interface

        public async Task<OperationResult<EmailNotificationResponse>> CreateEmailNotificationAsync(EmailNotificationRequest request)
        {            
            var res = await _dbContextOperationExecutor.ExecuteAsync<EmailNotificationResponse>(async (ctx) =>
            {
                if (request == null || request.Recipients == null
                    || !request.Recipients.Any() || !request.TemplateID.HasValue)
                    throw new NotSupportedException("Invalid input params");

                EmailMessage email = await PrepareEmail(request.TemplateID.Value, request.Parameters, request.Priority);

                if (request.Transliterate == true)
                    _emailService.AddEmailBodyTransliteration(email, PARAM_TRANSLITERATION_NAME);

                var createMailResult = await CreateMailAsync(email, request.SeparateMailPerRecipient, request.Recipients);

                return createMailResult;
            });

            return res;
        }

        #endregion

        #region Helpers

        private async Task<EmailMessage> PrepareEmail(int templateID, Dictionary<string, string> parameters, EmailPriority? priority)
        {
            await _emailsCache.EnsureLoadedAsync(CancellationToken.None);

            EmailTemplate template = _emailsCache.GetEmailTemplate(templateID);

            if (template == null || String.IsNullOrEmpty(template.Subject) || String.IsNullOrEmpty(template.Body))
                throw new NotSupportedException("Invalid template");

            var email = new EmailMessage() { Subject = template.Subject, Body = template.Body, IsBodyHtml = template.IsBodyHtml == true, Priority = priority ?? EmailPriority.Normal, TryCount = 10, SendingProviderName = "public" };

            if (parameters != null)
                foreach (var param in parameters)
                {
                    //prepare param key
                    string paramKey = param.Key;
                    paramKey = paramKey.StartsWith("{") ? paramKey : $"{{{paramKey}";
                    paramKey = paramKey.EndsWith("}") ? paramKey : $"{paramKey}}}";

                    email.Body = email.Body.Replace(paramKey, param.Value);
                    email.Subject = email.Subject.Replace(paramKey, param.Value);
                }

            return email;
        }

        private Task<OperationResult<EmailNotificationResponse>> CreateMailAsync(EmailMessage email, bool? separateMailPerRecipient, EmailRecipient[] recipients)
        {
            var emailIDsList = new List<int>();
            return _dbContextOperationExecutor.ExecuteAsync(async (dbContext) =>
            {
                var result = new OperationResult<EmailNotificationResponse>(OperationResultTypes.SuccessfullyCompleted);
                if (separateMailPerRecipient == true)
                {
                    foreach (var recipient in recipients)
                    {
                        email.Recipients = new EmailRecipient[] { recipient };
                        email.EmailID = null;
                        var innerRes = await _emailService.CreateEmailAsync(email, CancellationToken.None);
                        if (!innerRes.IsSuccessfullyCompleted)
                        {
                            result.SetAsUnsuccessfull(innerRes.Errors);
                            return result;
                        }
                        emailIDsList.Add(email.EmailID.Value);
                    }
                }
                else
                {
                    email.Recipients = recipients ?? null;
                    

                    var innerRes = await _emailService.CreateEmailAsync(email, CancellationToken.None);
                    if (!innerRes.IsSuccessfullyCompleted)
                    {
                        result.SetAsUnsuccessfull(innerRes.Errors);
                        return result;
                    }

                    emailIDsList.Add(email.EmailID.Value);
                }

                result.Result = new EmailNotificationResponse() { EmailIDs = emailIDsList };
                return result;
            });

        }

        #endregion
    }
}
