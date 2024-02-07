using CNSys.Data;
using CNSys.Hosting;
using EAU.Emails.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Emails
{
    /// <summary>
    /// Engine за изпращане на електронна поща
    /// </summary>
    public class EmailSenderEngine : PollingBackgroundService
    {
        #region Fields

        private IEmailService _emailService;

        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;
        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public EmailSenderEngine(ILogger<EmailSenderEngine> logger, IServiceProvider serviceProvider) : base(logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            _emailService = _serviceProvider.GetRequiredService<IEmailService>();
            _dbContextOperationExecutor = _serviceProvider.GetRequiredService<IDbContextOperationExecutor>();

            var emailOptions = _serviceProvider.GetRequiredService<IOptionsMonitor<EmailOptions>>().CurrentValue;

            NextDelayCalculator = GetDefaultCalculatorWithIncrementalBackoff(
                emailOptions.EML_POLLING_INTERVAL,
                emailOptions.EML_POLLING_INTERVAL,
                new TimeSpan(emailOptions.EML_POLLING_INTERVAL.Ticks * 10));
        }

        protected override Task PollAsync(CancellationToken stoppingToken)
        {
            return SendPendingEmails(stoppingToken);
        }

        #endregion

        #region Helpers

        private async Task SendPendingEmails(CancellationToken token)
        {
            using (var scopedServices = _serviceProvider.CreateScope())
            {
                var emailsService = scopedServices.ServiceProvider.GetRequiredService<IEmailService>();

                var emailOptions = scopedServices.ServiceProvider.GetRequiredService<IOptionsMonitor<EmailOptions>>().CurrentValue;

                var sendingService = scopedServices.ServiceProvider.GetRequiredService<IEmailsSendingService>();

                bool doPump = true;

                /*pump till there is messages sent*/
                while (doPump && !token.IsCancellationRequested)
                {
                    var operationResult = await _dbContextOperationExecutor.ExecuteAsync(async (dbContext, innerToken) =>
                    {
                        var ret = new CNSys.OperationResult<int>(CNSys.OperationResultTypes.SuccessfullyCompleted) { Result = 0 };

                        try
                        {
                            /*Get and lock pending mails*/
                            List<EmailMessage> mailsToSend = await emailsService.GetPendingEmailsAsync(emailOptions.EML_MAX_EMAILS_FETCHED, innerToken);
                            int mailsSent = 0;

                            #region Process Mail Messages

                            if (mailsToSend.Count > 0)
                            {
                                /*Try send mails */
                                var emailSendingResult = await sendingService.TrySendEmailsAsync(mailsToSend, innerToken);

                                #region Set mail sending result

                                foreach (var result in emailSendingResult)
                                {
                                    try
                                    {
                                        if (!result.IsSent)
                                            _logger.LogError(result.Exception, "Error on sending message with ID {ID}!", result.Mail.EmailID);
                                        else
                                            _logger.LogInformation("Message with ID {EmailID} sent.", result.Mail.EmailID);

                                        mailsSent += result.IsSent ? 1 : 0;

                                        await emailsService.EmailSendAttemptAsync(result.Mail.EmailID.Value, result.IsSent, innerToken);
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogException(ex);
                                    }
                                }

                                #endregion
                            }

                            #endregion

                            ret.Result = mailsSent;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogException(ex);
                        }

                        return ret;
                    }, token);

                    doPump = operationResult.Result > 0;
                }
            }
        }

        #endregion
    }
}
