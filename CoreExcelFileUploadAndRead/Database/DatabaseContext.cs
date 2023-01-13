using CoreExcelFileUploadAndRead.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreExcelFileUploadAndRead.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

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
                entity.Property(x => x.UploadTime).HasDefaultValueSql("GETDATE()");
                entity.Property(x => x.OpeningBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.OpeningBalancePassive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverDebit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverCredit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalancePassive).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(x => x.OpeningBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.OpeningBalancePassive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverDebit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverCredit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalancePassive).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ClassGroup>(entity =>
            {
                entity.Property(x => x.OpeningBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.OpeningBalancePassive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverDebit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverCredit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalancePassive).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<BalanceAccount>(entity =>
            {
                entity.Property(x => x.OpeningBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.OpeningBalancePassive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverDebit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.TurnoverCredit).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalanceActive).HasColumnType("decimal(18,2)");
                entity.Property(x => x.ClosingBalancePassive).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<FileData>();
        }
    }
}
