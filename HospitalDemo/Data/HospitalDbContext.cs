using Microsoft.EntityFrameworkCore;
using NodaTime.Extensions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;
using NodaTime;
using System.Xml;
using HospitalDemo.Models.UOM;
using HospitalDemo.Models.Patient;
using HospitalDemo.Models.User;
using HospitalDemo.Models.Category;
using Npgsql;
using HospitalDemo.Models.Bill;
using HospitalDemo.Models.InventoryItem;
using HospitalDemo.Models.Transactiontype;
using HospitalDemo.Models.SalesServiceItem;
using HospitalDemo.Models.DailyClosing;
using HospitalDemo.Models.InventoryTransaction;
using HospitalDemo.Models.Deposit;
using HospitalDemo.Models.BillItem;
using HospitalDemo.Models.ClosingBillDetail;
using HospitalDemo.Models.ClosingDepositdetail;
using HospitalDemo.Models.Payment;
using HospitalDemo.Models.DepositUsed;
using HospitalDemo.Models.RefreshToken;

namespace HospitalDemo.Data
{
    public class HospitalDbContext:DbContext
    {

        public HospitalDbContext(DbContextOptions options):base(options)
        {
            
        }
        [Obsolete]
        static HospitalDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<gender_enum>();
        }
           
       

        public DbSet<Patient> patient { get; set; }
        public DbSet<UserLogin> User { get; set; }
        public DbSet<UOM> uom { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Bill> bill { get; set; }
        public DbSet<Inventoryitem> inventoryitem { get; set; }
        public DbSet<Transactiontype> transactiontype { get; set; }
        public DbSet<Salesserviceitem> salesserviceitem { get; set; }
        public DbSet<Dailyclosing> dailyclosing { get; set; }
        public DbSet<Inventorytransactions> inventorytransaction { get; set; }
        public DbSet<Deposit> deposit { get; set; }
        public DbSet<Billitem> billitem { get; set; }
        public DbSet<Closingbilldetail> closingbilldetail { get; set; }
        public DbSet<Closingdepositdetail> closingdepositdetail { get; set; }
        public DbSet<Payment> payment { get; set; }
        public DbSet<Depositused> depositused { get; set; }
        public DbSet<refreshToken> RefreshToken { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Server=localhost;Database=hospital_api;Username=postgres;Password=sawwinthant", o => o.UseNodaTime());
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Inventoryitem>()
                 .HasKey(e => e.id);

            modelBuilder.Entity<refreshToken>()
                 .HasKey(e => e.Token_id);

            modelBuilder.Entity<Inventoryitem>()
                    .Property(e => e.id)
                    .HasColumnName("id");

            //  modelBuilder.HasPostgresExtension("uuid-ossp");
            // modelBuilder.HasPostgresEnum<gender_enum>("gender_enum");


            //modelBuilder
            //    .Entity<Patient>()
            //    .Property(e => e.gender)
            //    .HasConversion(
            //        v => v.ToString(),
            //        v => (gender_enum)Enum.Parse(typeof(gender_enum), v))
            //        .HasColumnType("gender_enum");


            //modelBuilder.Entity<UOM>()
            //            .Property(x => x.created_time)
            //            .HasColumnType("timestamp with time zone");

            //modelBuilder.Entity<UOM>()
            //            .Property(x => x.updated_time)
            //            .HasColumnType("timestamp with time zone");

            //modelBuilder.Entity<Patient>()
            //    .Property(e => e.Id)
            //    .HasConversion(
            //        v => v,
            //        v => (Guid)v);

            //modelBuilder.Entity<UserLogin>()
            //   .Property(e => e.Id)
            //   .HasConversion(
            //       v => v,
            //       v => (Guid)v);



        }
    }
}
