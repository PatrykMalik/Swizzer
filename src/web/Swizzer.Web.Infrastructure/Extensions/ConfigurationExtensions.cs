using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Web.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration)
            where TSettings : new()
        {
            var section = typeof(TSettings).Name.Replace("Settings", string.Empty);
            var settings = new TSettings();
            configuration.GetSection(section).Bind(settings);

            return settings;
        }
    }
}
