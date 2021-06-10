using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Pointwest.Exam.Shared;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pointwest.Exam.Domain.Models
{
    public class ApplicationContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILogger<ApplicationContext> _log;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IDateTimeService dateTime, ILogger<ApplicationContext> log) : base(options)
        {
            _dateTime = dateTime;
            _log = log;
        }
        public DbSet<Employee> Employee { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
        public bool HasChanges => ChangeTracker.HasChanges();
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        _log.LogInformation("The {Entity} has been {State}", entry.Entity, entry.State);
                        break;

                    case EntityState.Modified:
                        _log.LogInformation("The {Entity} has been {State}", entry.Entity, entry.State);
                        break;

                    case EntityState.Deleted:
                        _log.LogWarning("The {Entity} has been {State}", entry.Entity, entry.State);
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
