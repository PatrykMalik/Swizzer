using Swizzer.Shared.Providers;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Users.Models
{
    public class User : IIdProvider,
        ICreatedAtProvider
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();

        public ICollection<Message> RecivedMessages { get; set; } = new HashSet<Message>();
    }
}
