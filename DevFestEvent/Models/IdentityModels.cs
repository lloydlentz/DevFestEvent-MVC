using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DevFestEvent.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<DevFestEvent.Models.Session> Sessions { get; set; }
        public System.Data.Entity.DbSet<DevFestEvent.Models.Speaker> Speakers { get; set; }
        public System.Data.Entity.DbSet<DevFestEvent.Models.AppUser> AppUsers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(c => c.Sessions)
                .WithMany(a => a.AppUsers)
                .Map(m =>
                {
                    m.MapLeftKey("UserID");
                    m.MapRightKey("ID");
                    m.ToTable("AppUserSessions");
                });
        }
    }


}