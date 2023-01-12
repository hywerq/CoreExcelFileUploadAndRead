using CoreExcelFileUploadAndRead.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace CoreExcelFileUploadAndRead.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<ExcelFile> Files { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassGroup> ClassGroups { get; set; }
        public DbSet<BalanceAccount> BalanceAccounts { get; set; }
        public DbSet<FileData> FileDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcelFile>(entity =>
            {
                entity.Property(x => x.PeriodStart).HasColumnType("date");
                entity.Property(x => x.PeriodEnd).HasColumnType("date");
                entity.Property(u => u.UploadTime).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Class>();
            modelBuilder.Entity<ClassGroup>();
            modelBuilder.Entity<BalanceAccount>();
            modelBuilder.Entity<FileData>().HasNoKey();
        }
    }
}
