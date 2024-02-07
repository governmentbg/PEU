using EAU.Emails.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Emails
{
    public class SendingMailResult
    {
        public SendingMailResult(EmailMessage mail, bool isSent, Exception exception)
        {
            Mail = mail;
            IsSent = isSent;
            Exception = exception;
        }

        public EmailMessage Mail { get; }
        public bool IsSent { get; }
        public Exception Exception { get; }
    }
    /// <summary>
    /// Интерфейс на услуга за изпращане на имейли.
    /// </summary>
    public interface IEmailsSendingService
    {
        /// <summary>
        /// Изпращане на имейли .
        /// </summary>
        /// <param name="mails">имейли, които да бъдат изпратени.</param>
        /// <param name="cancellationToken">жетон за прекратяване на операцията</param>
        /// <returns></returns>
        Task<IEnumerable<SendingMailResult>> TrySendEmailsAsync(IEnumerable<EmailMessage> mails, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс IEmailsSendingService за изпращане на имейли.
    /// </summary>
    internal class EmailsSendingService : IEmailsSendingService
    {
        #region Private members

        private readonly ILogger _logger;
        private readonly EmailOptions _emailsOptions;

        #endregion

        #region Constructor

        public EmailsSendingService(ILogger<EmailsSendingService> logger,
            IOptionsMonitor<EmailOptions> emailOptions)
        {
            _logger = logger;
            _emailsOptions = emailOptions.CurrentValue;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Операция за опит за изпращане на имейл.
        /// </summary>
        /// <param name="mail"></param>
        public async Task<IEnumerable<SendingMailResult>> TrySendEmailsAsync(IEnumerable<EmailMessage> mails, CancellationToken cancellationToken)
        {
            List<SendingMailResult> ret = new List<SendingMailResult>();
            using (SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync(_emailsOptions.EML_SMTP_HOST, _emailsOptions.EML_SMTP_PORT, MailKit.Security.SecureSocketOptions.Auto, cancellationToken);

                try
                {
                    if (!string.IsNullOrEmpty(_emailsOptions.EML_SMTP_USER))
                        await client.AuthenticateAsync(_emailsOptions.EML_SMTP_USER, _emailsOptions.EML_SMTP_PASSWORD, cancellationToken);

                    foreach (var mail in mails)
                    {
                        /*Check if the client is connected If there is an error, then the client may disconnect */
                        if (!client.IsConnected)
                            break;

                        #region CreateAndSendMail

                        try
                        {
                            if (mail.Status != EmailStatues.Pending || (mail.Recipients?.Length).GetValueOrDefault() == 0)
                                throw new NotSupportedException("mail status not pending or no addresses");

                            var message = CreateMailMessage(mail);

                            await client.SendAsync(message, cancellationToken);

                            ret.Add(new SendingMailResult(mail, true, null));
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }
                        catch (SmtpCommandException commandException)
                        {
                            if (commandException.StatusCode == SmtpStatusCode.ServiceNotAvailable)
                            {
                                _logger.LogWarning("Stopped sending mails to MailServer - ServiceNotAvailable! Server Message: {ServerMessage}!", commandException.Message);
                                break;
                            }
                            else
                                ret.Add(new SendingMailResult(mail, false, commandException));
                        }
                        catch (Exception exep)
                        {
                            ret.Add(new SendingMailResult(mail, false, exep));
                        }

                        #endregion
                    }
                }
                finally
                {
                    await client.DisconnectAsync(true, cancellationToken);
                }
            }

            return ret;
        }

        #endregion

        #region Private Methods

        MimeMessage CreateMailMessage(EmailMessage mail)
        {
            MimeMessage message = new MimeMessage();

            BodyBuilder bodyBuilder = new BodyBuilder();

            if (mail.IsBodyHtml)
                bodyBuilder.HtmlBody = mail.Body;
            else
                bodyBuilder.TextBody = mail.Body;

            message.Body = bodyBuilder.ToMessageBody();

            message.From.Add(InternetAddress.Parse(_emailsOptions.EML_SEND_FROM));

            message.Subject = mail.Subject;

            if (mail.Priority == EmailPriority.Normal)
                message.Priority = MessagePriority.Normal;
            else if (mail.Priority == EmailPriority.Low)
                message.Priority = MessagePriority.NonUrgent;
            else if (mail.Priority == EmailPriority.High)
                message.Priority = MessagePriority.Urgent;

            foreach (var recipient in mail.Recipients)
            {
                if (recipient.Type == AddressTypes.To)
                    message.To.Add(new MailboxAddress(recipient.DisplayName, recipient.Address));
                else if (recipient.Type == AddressTypes.Cc)
                    message.Cc.Add(new MailboxAddress(recipient.DisplayName, recipient.Address));
                else if (recipient.Type == AddressTypes.Bcc)
                    message.Bcc.Add(new MailboxAddress(recipient.DisplayName, recipient.Address));
            }

            return message;
        }

        #endregion
    }
}
