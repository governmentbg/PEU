using Dapper;
using EAU.Services.DocumentProcesses.Models;
using EAU.Services.ServiceInstances.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Services
{
    public static class EAUServicesStartupBootstrapper
    {
        public static void Run()
        {
            EAUServicesStartupRegistrator.Current.Register();
        }
    }

    public class EAUServicesStartupRegistrator : StartupRegistrator<EAUServicesStartupRegistrator>
    {
        protected override void RegisterInternal()
        {
            SqlMapper.AddTypeHandler<AdditionalData>(new AdditionalDataDapperMapHandler());
            SqlMapper.SetTypeMap(typeof(AttachedDocument), DataContextHelper.ColumnMap<AttachedDocument>());
            SqlMapper.SetTypeMap(typeof(DocumentProcess), DataContextHelper.ColumnMap<DocumentProcess>());
            SqlMapper.SetTypeMap(typeof(DocumentProcessContent), DataContextHelper.ColumnMap<DocumentProcessContent>());
            SqlMapper.SetTypeMap(typeof(ServiceInstance), DataContextHelper.ColumnMap<ServiceInstance>());
        }
    }
}
