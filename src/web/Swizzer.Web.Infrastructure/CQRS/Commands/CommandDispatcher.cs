using Autofac;
using Swizzer.Shared.Providers;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task DispachAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider;
    }
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _componentContext;
        private readonly SwizzerContext _context;

        public CommandDispatcher(IComponentContext componentContext, SwizzerContext context)
        {
            _componentContext = componentContext;
            _context = context;
        }
        public async Task DispachAsync<TCommand>(TCommand command) 
            where TCommand : ICommandProvider
        {
            var handler = _componentContext.Resolve<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
            await _context.SaveChangesAsync();
            //można dodać w tym miejscu logowanie komend lub walidację komend
        }
    }
}
