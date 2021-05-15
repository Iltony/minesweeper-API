using Microsoft.EntityFrameworkCore;
using MWEntities;

namespace MWPersistence
{
    public class MWContext : DbContext
    {
        public MWContext(DbContextOptions<MWContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<PersistibleBoard> PersistibleBoards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u=> u.Username);

            modelBuilder.Entity<PersistibleBoard>()
                .ToTable("PersistibleBoards");

            modelBuilder.Entity<PersistibleBoard>()
                .HasKey(u => new { u.Username, u.BoardId });
        }

    }
}
