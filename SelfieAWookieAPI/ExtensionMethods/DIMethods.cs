using Microsoft.Extensions.DependencyInjection;
using SelfieAWookie.Core.Domain;
using SelfieAWookie.Core.Infrastructure.Repository;

namespace SelfieAWookieAPI.ExtensionMethods
{
    public static class DiMethods
    {
        #region Public methods

        /// <summary>
        ///     Prepare custom dependency injection
        /// </summary>
        /// <param name="services"></param>
        public static void AddInjection(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
        }

        #endregion
    }
}