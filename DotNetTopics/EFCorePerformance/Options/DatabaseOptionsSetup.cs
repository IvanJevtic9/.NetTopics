using Microsoft.Extensions.Options;

namespace EFCorePerformance.Options
{
    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
    {
        private const string ConfigurationSectionName = "DatabaseOptions";
        private readonly IConfiguration _configuration;
        public void Configure(DatabaseOptions options)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            _configuration.GetSection(ConfigurationSectionName).Bind(options);

            options.ConnectionString = connectionString;
        }
    }
}
