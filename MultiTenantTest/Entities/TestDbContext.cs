using Microsoft.EntityFrameworkCore;

namespace MultiTenantTest.Entities
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
            
        }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<TUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TUser>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
