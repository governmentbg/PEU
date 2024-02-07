using Dapper;
using EAU.Signing.Models;
using EAU.Utilities;

namespace EAU.Signing
{
    public static class SigningModuleDapperBootstrapper
    {
        public static void Run()
        {
            SigningModuleDapperRegistrator.Current.Register();
        }
    }

    internal class SigningModuleDapperRegistrator : StartupRegistrator<SigningModuleDapperRegistrator>
    {
        protected override void RegisterInternal()
        {
            SqlMapper.AddTypeHandler<RemoteSignRequestAdditionalData>(new RemoteSignRequestAdditionalDataDapperMapHandler());
            SqlMapper.AddTypeHandler<SignProcessAdditionalData>(new SignProcessAdditionalDataDapperMapHandler());
            SqlMapper.SetTypeMap(typeof(SigningProcess), DataContextHelper.ColumnMap<SigningProcess>());
            SqlMapper.SetTypeMap(typeof(Signer), DataContextHelper.ColumnMap<Signer>());
        }
    }
}
