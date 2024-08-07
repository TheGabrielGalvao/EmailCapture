using Domain.Entities;
using Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public  class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region DbSet
        public DbSet<User> Users { get; set; }

        #endregion


        public override int SaveChanges()
        {
            GenerateGuidForNewEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            GenerateGuidForNewEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void GenerateGuidForNewEntities()
        {
            var entitiesWithDefaultEntity = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is DefaultEntity)
                .Select(e => e.Entity as DefaultEntity)
                .ToList();

            foreach (var entity in entitiesWithDefaultEntity)
            {
                if (entity.Uuid == Guid.Empty)
                {
                    entity.Uuid = Guid.NewGuid();
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            
        }
    }
}
