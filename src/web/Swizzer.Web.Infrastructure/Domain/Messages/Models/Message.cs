using Swizzer.Shared.Providers;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Domain.Messages.Models
{
    public class Message : IIdProvider, ICreatedAtProvider
    {
        public Guid Id { get ; set ; }
        public DateTime CreateAt { get ; set; }

        public string Content { get; set; }

        public Guid SenderId { get; set; }

        public Guid RecieverId { get; set; }

        public User Sender { get; set; }

        public User Reciever { get; set; }
    }
}
