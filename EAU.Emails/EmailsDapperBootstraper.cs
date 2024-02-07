using Dapper;
using EAU.Emails.Models;
using EAU.Utilities;

namespace EAU.Emails
{
    public static class EmailsDapperBootstraper
    {
        public static void Run()
        {
            EmailsDapperBootstraperRegistrator.Current.Register();
        }
    }

    internal class EmailsDapperBootstraperRegistrator : StartupRegistrator<EmailsDapperBootstraperRegistrator>
    {
        protected override void RegisterInternal()
        {
            SqlMapper.AddTypeHandler(new EmailRecipientDapperMapHandler());

            SqlMapper.SetTypeMap(typeof(EmailMessage), DataContextHelper.ColumnMap<EmailMessage>());
            SqlMapper.SetTypeMap(typeof(EmailTemplate), DataContextHelper.ColumnMap<EmailTemplate>());
        }
    }
}
