using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;
using Swizzer.Web.Infrastructure.CQRS;
using Swizzer.Web.Infrastructure.Framework;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Sql;

namespace Swizzer.Web.Infrastructure.IoC
{
    public class MainModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public MainModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new SqlModule(_configuration));
            builder.RegisterModule(new FrameworkModule(_configuration));
            builder.RegisterModule(new MapperModule());
            builder.RegisterModule(new CqrsModule());

            base.Load(builder);
        }
    }
}
