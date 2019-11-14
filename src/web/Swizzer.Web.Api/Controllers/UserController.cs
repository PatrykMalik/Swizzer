using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swizzer.Shared.Domain.Users.Commands;
using Swizzer.Shared.Domain.Users.Dto;
using Swizzer.Web.Infrastructure.CQRS.Commands;
using Swizzer.Web.Infrastructure.Framework.Caching;

namespace Swizzer.Web.Api.Controllers
{
    public class UserController : SwizzerControllerBase
    {
        public UserController(ICommandDispatcher commandDispatcher, ICacheService cacheService) : base(commandDispatcher, cacheService)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync ([FromBody] CreateUserCommand command)
        {
            await DispachCommandAsync(command);
            var dto = GetCachedObject<UserDto>(command.Id);
            return Created($"api/users/{command.Id}", dto);
        }
    }
}
