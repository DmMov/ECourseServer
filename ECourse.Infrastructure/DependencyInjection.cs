using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ECourse.Application.Interfaces;
using ECourse.Infrastructure.Services;
using ECourse.Infrastructure.Identity;

namespace ECourse.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECourseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ECourseConnection"),
                    b => b.MigrationsAssembly(typeof(ECourseContext).Assembly.FullName)
               )
            );

            services.AddScoped<IECourseContext>(provider => provider.GetService<ECourseContext>());
            services.AddScoped<IDateTime, DateTimeService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileService, FileService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IMailSenderService, MailSenderService>();

            return services;
        }
    }
}
