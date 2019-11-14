using Swizzer.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Domain.Users.Commands
{
    public class CreateUserCommand : ICommandProvider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
