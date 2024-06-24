using Microsoft.EntityFrameworkCore;

namespace GasWayLauncher.Model
{
    public class ContextBD : DbContext
    {
        public DbSet<UserInformation> UserInfo { get; set; }
        public DbSet<UserMessages> UserMess { get; set; }

        public ContextBD() : base()
        {
        }

        public ContextBD(DbContextOptions<ContextBD> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\PROJECTS\\Gas Way\\Launcher\\ProjectFolder\\GasWayLauncher\\GasWayLauncher\\Data\\LauncherDatabase.mdf\";Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserMessages>()
                .HasOne(um => um.UserInformation)
                .WithMany(ui => ui.UserMessages)
                .HasForeignKey(um => um.username_id)
                .HasConstraintName("FK_UserMessages_UserInformation");
        }
    }
}
