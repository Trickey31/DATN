using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Workspace.Infrastructure;

namespace Workspace.API
{
    public static class JwtExtensions
    {
        public static void AddJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                JwtOptions options = new ();
                configuration.GetSection(nameof(JwtOptions)).Bind(options);

                var key = Encoding.UTF8.GetBytes("AKDgyX1MCm6JomJ2st1qaVa5YdDrhyZT");
                o.SaveToken = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // true in production
                    ValidateAudience = false, // true in production
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidAudience = options.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                };

                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("IS-TOKEN-EXPIRED", "true");
                        }

                        return Task.CompletedTask;
                    },
                };
            });

            services.AddAuthorization();
        }
    }
}
