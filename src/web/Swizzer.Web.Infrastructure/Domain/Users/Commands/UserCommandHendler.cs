using Microsoft.EntityFrameworkCore;
using Swizzer.Shared.Domain.Users.Commands;
using Swizzer.Shared.Domain.Users.Dto;
using Swizzer.Shared.Exceptions;
using Swizzer.Web.Infrastructure.CQRS.Commands;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using Swizzer.Web.Infrastructure.Framework.Caching;
using Swizzer.Web.Infrastructure.Framework.Security;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Domain.Users.Commands
{
    class UserCommandHendler : ICommandHandler<CreateUserCommand>
    {
        private readonly ISecurityService _securityService;
        private readonly ICacheService _cacheService;
        private readonly ISwizzerMapper _swizzerMapper;
        private readonly SwizzerContext _swizzerContext;

        public UserCommandHendler(ISecurityService securityService,  ICacheService cacheService, ISwizzerMapper swizzerMapper, SwizzerContext swizzerContext)
        {
            _securityService = securityService;
            _cacheService = cacheService;
            _swizzerMapper = swizzerMapper;
            _swizzerContext = swizzerContext;
        }
        public async Task HandleAsync(CreateUserCommand command)
        {
            var user = await _swizzerContext.Users.FirstOrDefaultAsync(x => x.Email == command.Email);

            if (user != null)
            {
                throw new SwizzerSerwerException(ErrorCodes.InvalidParameter,
                    $"{command.Email} already exist with ${user.Id}");
            }

            user = _swizzerMapper.MapTo<User>(command);

            user.Salt = _securityService.GetSalt();
            user.Hash = _securityService.GetHash(command.Password, user.Salt);

            await _swizzerContext.AddAsync(user);
            var dto = _swizzerMapper.MapTo<UserDto>(user);
            _cacheService.Set(dto);
        }
    }
}
