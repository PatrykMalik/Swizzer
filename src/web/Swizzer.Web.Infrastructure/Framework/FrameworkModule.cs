using Autofac;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Swizzer.Web.Infrastructure.Extensions;
using Swizzer.Web.Infrastructure.Framework.Caching;
using Swizzer.Web.Infrastructure.Framework.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Framework
{
    public class FrameworkModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public FrameworkModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var cacheSettings = _configuration.GetSettings<CacheSettings>();

            builder.RegisterInstance(cacheSettings)
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<MemoryCache>()
                .As<IMemoryCache>()
                .SingleInstance();

            builder.RegisterType<CacheService>()
                .As<ICacheService>()
                .SingleInstance();

            var securitySettings = _configuration.GetSettings<SecuritySettings>();

            builder.RegisterInstance(securitySettings)
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<SecurityService>()
                .As<ISecurityService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
