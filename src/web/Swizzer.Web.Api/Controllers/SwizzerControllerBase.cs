using Microsoft.AspNetCore.Mvc;
using Swizzer.Shared.Exceptions;
using Swizzer.Shared.Providers;
using Swizzer.Web.Infrastructure.CQRS.Commands;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Framework.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swizzer.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class SwizzerControllerBase : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly ICommandDispatcher _commandDispatcher;
        protected Guid UserId => User?.Identity?.IsAuthenticated == true
            ? Guid.Parse(User.Identity.Name)
            : Guid.Empty;
        public SwizzerControllerBase(ICommandDispatcher commandDispatcher, ICacheService cacheService)
        {
            _commandDispatcher = commandDispatcher;
            _cacheService = cacheService;
        }

        protected async Task DispachCommandAsync<TCommand>(TCommand command)
            where TCommand : ICommandProvider
        {
            Authorize(command);

            await _commandDispatcher.DispachAsync(command);
        }

        private void Authorize<TRequest>(TRequest command)
        {
            if (command is IAuthenticatedRequest authenticatedRequest)
            {
                authenticatedRequest.RequestBy = UserId;

                if (authenticatedRequest.RequestBy == Guid.Empty)
                {
                    throw new SwizzerSerwerException(ErrorCodes.Unauthorized);
                }
            }
        }
        protected TEntity GetCachedObject<TEntity>(object key)
            where TEntity : IIdProvider
            => _cacheService.Get<TEntity>(key);
    }
}
