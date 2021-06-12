using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SelfieAWookie.Core.Infrastructure.Configuration;

namespace SelfieAWookieAPI.ExtensionMethods
{
    /// <summary>
    ///     Custom options from config (json)
    /// </summary>
    public static class OptionsMethods
    {
        #region Public methods

        /// <summary>
        ///     Add custom options
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }

        #endregion
    }
}