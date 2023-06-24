#nullable disable

namespace EtarChallenge.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasOne(c => c.user)
                .WithMany()
                .HasForeignKey(c => c.createdBy);

            modelBuilder.Entity<Item>()
                .HasOne(c => c.category)
                .WithMany()
                .HasForeignKey(c => c.catId);

            modelBuilder.Entity<Item>()
                .HasOne(c => c.user)
                .WithMany()
                .HasForeignKey(c => c.createdBy);
        }
    }
}