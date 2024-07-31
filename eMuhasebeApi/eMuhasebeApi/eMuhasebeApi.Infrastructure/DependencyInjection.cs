using eMuhasebeApi.Application.Services;
using eMuhasebeApi.Domain.Entities;
using eMuhasebeApi.Domain.Repositories;
using eMuhasebeApi.Infrastructure.Context;
using eMuhasebeApi.Infrastructure.Options;
using eMuhasebeApi.Infrastructure.Services;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace eMuhasebeApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache(); //memory cache için
            services.AddScoped<ICacheService, MemoryCacheService>();

            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost")); //redis cache için 
            //services.AddScoped<ICacheService, RedisCacheService>();


            services.AddScoped<CompanyDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWorkCompany>(srv => srv.GetRequiredService<CompanyDbContext>());

            services
                .AddIdentity<AppUser, IdentityRole<Guid>>(cfr =>
                {
                    cfr.Password.RequiredLength = 1;
                    cfr.Password.RequireNonAlphanumeric = false;
                    cfr.Password.RequireUppercase = false;
                    cfr.Password.RequireLowercase = false;
                    cfr.Password.RequireDigit = false;
                    cfr.SignIn.RequireConfirmedEmail = true;
                    cfr.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    cfr.Lockout.MaxFailedAccessAttempts = 3;
                    cfr.Lockout.AllowedForNewUsers = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtTokenOptionsSetup>();
            services.AddAuthentication()
                .AddJwtBearer();
            services.AddAuthorizationBuilder();

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
            });

            return services;
        }
    }
}
