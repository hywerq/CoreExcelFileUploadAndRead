// <auto-generated />
using System;
using CoreExcelFileUploadAndRead.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreExcelFileUploadAndRead.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.BalanceAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ClosingBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClosingBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("OpeningBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpeningBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TurnoverCredit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("BalanceAccounts");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.ClassGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ClosingBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClosingBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("OpeningBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpeningBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TurnoverCredit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ClassGroups");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.ExcelFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ClosingBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClosingBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OpeningBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpeningBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PeriodEnd")
                        .HasColumnType("date");

                    b.Property<DateTime>("PeriodStart")
                        .HasColumnType("date");

                    b.Property<DateTime>("PrintTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TurnoverCredit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UploadTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.FileClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("ClosingBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ClosingBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpeningBalanceActive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OpeningBalancePassive")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TurnoverCredit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TurnoverDebit")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BalanceAccountId")
                        .HasColumnType("int");

                    b.Property<int?>("ClassGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.Property<int?>("ExcelFileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BalanceAccountId")
                        .IsUnique()
                        .HasFilter("[BalanceAccountId] IS NOT NULL");

                    b.HasIndex("ClassGroupId");

                    b.HasIndex("ClassId");

                    b.HasIndex("ExcelFileId");

                    b.ToTable("FileDatas");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.FileData", b =>
                {
                    b.HasOne("CoreExcelFileUploadAndRead.Database.Entities.BalanceAccount", "BalanceAccount")
                        .WithOne("FileData")
                        .HasForeignKey("CoreExcelFileUploadAndRead.Database.Entities.FileData", "BalanceAccountId");

                    b.HasOne("CoreExcelFileUploadAndRead.Database.Entities.ClassGroup", "ClassGroup")
                        .WithMany("FileDatas")
                        .HasForeignKey("ClassGroupId");

                    b.HasOne("CoreExcelFileUploadAndRead.Database.Entities.FileClass", "Class")
                        .WithMany("FileDatas")
                        .HasForeignKey("ClassId");

                    b.HasOne("CoreExcelFileUploadAndRead.Database.Entities.ExcelFile", "ExcelFile")
                        .WithMany("FileDatas")
                        .HasForeignKey("ExcelFileId");

                    b.Navigation("BalanceAccount");

                    b.Navigation("Class");

                    b.Navigation("ClassGroup");

                    b.Navigation("ExcelFile");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.BalanceAccount", b =>
                {
                    b.Navigation("FileData")
                        .IsRequired();
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.ClassGroup", b =>
                {
                    b.Navigation("FileDatas");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.ExcelFile", b =>
                {
                    b.Navigation("FileDatas");
                });

            modelBuilder.Entity("CoreExcelFileUploadAndRead.Database.Entities.FileClass", b =>
                {
                    b.Navigation("FileDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
