using EAU.Emails.Models;
using EAU.Emails.Repositories;
using EAU.Utilities;
using CNSys.Caching;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAU.Utilities.Caching;

namespace EAU.Emails.Cache
{
    internal class EmailsTemplateDbDataCache : DbDataCacheItem<List<EmailTemplate>>, IEmailsCache
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public EmailsTemplateDbDataCache(
            ILogger<EmailsTemplateDbDataCache> logger,
            IDbCacheInvalidationDispatcher dbCacheInvalidationDispatcher,
            IEmailTemplateRepository emailTemplateRepository) :
            base(logger,
                dbCacheInvalidationDispatcher,
                null,
                new string[] { "[eml].[n_s_email_templates]" })
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public ValueTask EnsureLoadedAsync(CancellationToken cancellationToken) => this.EnsureCreatedAsync(cancellationToken);

        public EmailTemplate GetEmailTemplate(int emailTemplateID) => Get().Value.FirstOrDefault(et => et.TemplateID == emailTemplateID);

        protected override async Task<CachedDataInfo<List<EmailTemplate>>> GenerateCacheDataInfoAsync(DateTime? etag, CancellationToken cancellationToken)
        {
            var data = await _emailTemplateRepository.SearchAsync(new EmailTemplateSearchCriteria(), cancellationToken);

            return new CachedDataInfo<List<EmailTemplate>>()
            {
                Value = data.ToList()
            };
        }
    }
}
