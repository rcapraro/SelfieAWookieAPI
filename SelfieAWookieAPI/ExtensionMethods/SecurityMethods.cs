using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SelfieAWookieAPI.ExtensionMethods
{
    /// <summary>
    ///     About security (cors, jwt)
    /// </summary>
    public static class SecurityMethods
    {
        #region Public methods

        /// <summary>
        ///     Add cors and jwt configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomCors(configuration);
            services.AddCustomAuthentication(configuration);
        }

        private static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(options =>
            {
                var jwtKey = configuration["Jwt:Key"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true
                };
                options.SaveToken = true;
            });
        }

        private static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy(DefaultPolicy, builder =>
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
                    options.AddPolicy(TestPolicy, builder =>
                        builder.WithOrigins(configuration["Cors:Origin"])
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
                }
            );
        }

        #endregion

        #region Constants

        public const string DefaultPolicy = "DEFAULT_POLICY";
        public const string TestPolicy = "TEST_POLICY";

        #endregion
    }
}