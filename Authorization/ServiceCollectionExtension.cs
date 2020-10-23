using Authorization.Services;
using Authorization.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Authorization
{
    public static class ServiceCollectionExtension
    {
        private static IConfiguration Configuration;

        public static IServiceCollection AddAuthorizationLibraryServices(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;

            services.AddControllers();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JwtToken:Issuer"],
                        ValidAudience = Configuration["JwtToken:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddCookie(Options =>
                {
                    Options.LoginPath = "/google-login";
                    Options.LoginPath = "/facebook-login";
                })
                .AddGoogle(Options =>
                {
                    Options.ClientId = Configuration["Authentication:Google:ClientId"];
                    Options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];

                    Options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddFacebook(Options =>
                {
                    Options.AppId = Configuration["Authentication:Facebook:AppId"];
                    Options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    Options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
