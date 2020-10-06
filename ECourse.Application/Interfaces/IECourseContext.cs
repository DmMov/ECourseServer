using Microsoft.EntityFrameworkCore;
using ECourse.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Interfaces
{
    public interface IECourseContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
