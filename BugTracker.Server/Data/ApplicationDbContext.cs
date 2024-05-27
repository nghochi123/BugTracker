using Microsoft.EntityFrameworkCore;

namespace Microsoft.BugTracker.Entities
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        // Define your DbSets (tables)
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany<ProjectUser>()
                .WithOne()
                .HasForeignKey(pu => pu.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasMany<Comment>()
                .WithOne()
                .HasForeignKey(pu => pu.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}