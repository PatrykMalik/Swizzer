using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Swizzer.Web.Infrastructure.CQRS.Commands;

namespace Swizzer.Web.Infrastructure.CQRS
{
    public class CqrsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(typeof(CqrsModule).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

        }
    }
}
