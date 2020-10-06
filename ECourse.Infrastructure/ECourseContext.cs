using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ECourse.Application.Interfaces;
using ECourse.Domain.Entities;
using System.Reflection;

namespace ECourse.Infrastructure
{
    public class ECourseContext : IdentityDbContext<User, IdentityRole<int>, int>, IECourseContext
    {
        public ECourseContext(DbContextOptions<ECourseContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
