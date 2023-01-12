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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ExcelFile>(entity =>
			{
                entity.Property(x => x.PeriodStart).HasColumnType("date");
				entity.Property(x => x.PeriodEnd).HasColumnType("date");
				entity.Property(x => x.OpeningBalanceActive).HasColumnType("decimal(20,2)");
				entity.Property(x => x.OpeningBalancePassive).HasColumnType("decimal(20,2)");
				entity.Property(x => x.TurnoverDebit).HasColumnType("decimal(20,2)");
				entity.Property(x => x.TurnoverCredit).HasColumnType("decimal(20,2)");
                entity.Property(x => x.ClosingBalanceActive).HasColumnType("decimal(20,2)");
                entity.Property(x => x.ClosingBalancePassive).HasColumnType("decimal(20,2)");
				entity.Property(u => u.UploadTime).HasDefaultValueSql("GETDATE()");
            });
		}
	}
}
