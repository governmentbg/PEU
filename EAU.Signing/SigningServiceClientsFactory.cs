using EAU.Signing.BSecureDSSL;
using EAU.Signing.BtrustRemoteClient;
using EAU.Signing.Evrotrust;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EAU.Signing
{
    public interface IBSecureDsslClientFactory
    {
        IBSecureDsslClient GetBSecureDsslClient();
    }

    public interface IBtrustRemoteClientFactory
    {
        IBtrustRemoteClient GetBtrustRemoteClient();
    }

    public interface IEvrotrustClientFactory
    {
        IEvrotrustClient GetEvrotrustClient();
    }

    internal class SigningServiceClientsFactory : IBSecureDsslClientFactory, IBtrustRemoteClientFactory, IEvrotrustClientFactory
    {
        private readonly IServiceProvider ServiceProvider;

        public SigningServiceClientsFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IBSecureDsslClient GetBSecureDsslClient()
        {
            return ServiceProvider.GetRequiredService<IBSecureDsslClient>();
        }

        public IBtrustRemoteClient GetBtrustRemoteClient()
        {
            return ServiceProvider.GetRequiredService<IBtrustRemoteClient>();
        }

        public IEvrotrustClient GetEvrotrustClient()
        {
            return ServiceProvider.GetRequiredService<IEvrotrustClient>();
        }
    }
}
