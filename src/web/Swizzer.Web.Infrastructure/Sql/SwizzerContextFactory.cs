using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Swizzer.Web.Infrastructure.Extensions;

namespace Swizzer.Web.Infrastructure.Sql
{
    public class SwizzerContextFactory : IDesignTimeDbContextFactory<SwizzerContext>
    {
        public SwizzerContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var sqlSettings = configuration.GetSettings<SqlSettings>();

            return new SwizzerContext(sqlSettings);
        }
    }
}
