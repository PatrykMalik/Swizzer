using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swizzer.Shared.Providers;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using Swizzer.Web.Infrastructure.Domain.Users.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Sql
{
    public class SwizzerContext : DbContext
    {
        private readonly SqlSettings _settings;

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public SwizzerContext(SqlSettings settings)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_settings.ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwizzerContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            BeforeSaveChanges();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void BeforeSaveChanges()
        {
            var changedEntities = ChangeTracker.Entries();
            foreach (var changedEntity in changedEntities)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        BeforeAdd(changedEntity.Entity);
                        break;
                }
            }
        }

        private void BeforeAdd(object changedEntity)
        {
            if (changedEntity is ICreatedAtProvider createdAtProvider)
            {
                createdAtProvider.CreateAt = DateTime.UtcNow;
            }
        }
    }
}
