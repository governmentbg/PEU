using Dapper;
using EAU.Payments.Obligations.Models;
using EAU.Utilities;

namespace EAU.Payments
{
    public static class EAUPaymentsStartupBootstrapper
    {
        public static void Run()
        {
            EAUPaymentsStartupRegistrator.Current.Register();
        }
    }

    public class EAUPaymentsStartupRegistrator : StartupRegistrator<EAUPaymentsStartupRegistrator>
    {
        protected override void RegisterInternal()
        {
            SqlMapper.SetTypeMap(typeof(Obligation), DataContextHelper.ColumnMap<Obligation>());
            SqlMapper.SetTypeMap(typeof(PaymentRequests.Models.PaymentRequest), DataContextHelper.ColumnMap<PaymentRequests.Models.PaymentRequest>());
            SqlMapper.AddTypeHandler<AdditionalData>(new AdditionalDataDapperMapHandler());
        }
    }
}
