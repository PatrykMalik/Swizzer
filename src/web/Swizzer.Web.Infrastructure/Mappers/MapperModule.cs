using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AutoMapper;

namespace Swizzer.Web.Infrastructure.Mappers
{
    class MapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var mapper = AutoMapperConfiguration.Initialize();

            builder.RegisterInstance(mapper)
                .As<IMapper>()
                .SingleInstance();

            builder.RegisterType<SwizzerMapper>()
                .As<ISwizzerMapper>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
