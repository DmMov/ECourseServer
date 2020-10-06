using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ECourse.Infrastructure;
using System;
using System.Threading.Tasks;
using ECourse.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ECourse.WebUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            ECourseContextSeed contextSeed = new ECourseContextSeed();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                try
                {
                    ECourseContext context = services.GetRequiredService<ECourseContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
                    
                    await contextSeed.SeedRolesAsync(context);
                    await contextSeed.SeedDefaultUserAsync(userManager);
                    await contextSeed.SeedCoursesAsync(context);
                }
                catch (Exception ex)
                {
                    ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}