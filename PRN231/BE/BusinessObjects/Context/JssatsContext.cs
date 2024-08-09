using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Context
{
    public class JssatsContext : DbContext
    {
        public JssatsContext()
        {
        }

        public JssatsContext(DbContextOptions<JssatsContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(GetConnectionString());
                //optionsBuilder.UseNpgsql(GetConnectionString());
                //optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=Abcd1234;Database=JSSATS;TrustServerCertificate=True");
                optionsBuilder.UseNpgsql("Host=aws-0-ap-southeast-1.pooler.supabase.com; Database=postgres; Username=postgres.gfjsnspjzlcfdrzxxksm; Password=Akaka0406+++");
            }
        }

        private static string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var strConn = config["ConnectionStrings:JSSATS-Cloud"];
            //var strConn = config["ConnectionStrings:JSSATS-Cloud"];

            if (string.IsNullOrEmpty(strConn))
            {
                throw new InvalidOperationException("Connection string 'JSSATS' not found.");
            }

            return strConn;
        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillJewelry> BillJewelries { get; set; }
        public DbSet<BillPromotion> BillPromotions { get; set; }

        public DbSet<Counter> Counters { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<JewelryType> JewelryTypes { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warranty> Warranties { get; set; }
        public DbSet<JewelryMaterial> JewelryMaterials { get; set; }

        public DbSet<Gold> Golds { get; set; }
        public DbSet<Gem> Gems { get; set; }
        
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Primary keys
            modelBuilder.Entity<Bill>()
                .HasKey(b => b.BillId);
            modelBuilder.Entity<BillJewelry>()
                .HasKey(bj => bj.BillJewelryId);
            modelBuilder.Entity<BillPromotion>()
                .HasKey(bp => bp.BillPromotionId);
            modelBuilder.Entity<Counter>()
                .HasKey(c => c.CounterId);
            modelBuilder.Entity<Customer>()
                .HasKey(cu => cu.CustomerId);
            modelBuilder.Entity<Gold>()
                .HasKey(gp => gp.GoldId);
            modelBuilder.Entity<Jewelry>()
                .HasKey(j => j.JewelryId);
            
            modelBuilder.Entity<Jewelry>()
                .HasIndex(j=>j.Barcode)
                .IsUnique();
            
            modelBuilder.Entity<JewelryType>()
                .HasKey(jt => jt.JewelryTypeId);
            modelBuilder.Entity<Promotion>()
                .HasKey(p => p.PromotionId);
            modelBuilder.Entity<Purchase>()
                .HasKey(p => p.PurchaseId);
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId);
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<Warranty>()
                .HasKey(w => w.WarrantyId);
            modelBuilder.Entity<Gem>()
                .HasKey(sp => sp.GemId);
            // Relationships

            modelBuilder.Entity<JewelryMaterial>()
                .HasOne(jm => jm.Jewelry)
                .WithMany(j => j.JewelryMaterials)
                .HasForeignKey(jm => jm.JewelryId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Warranty>()
                .HasOne(w => w.Jewelry)
                .WithOne(j => j.Warranty)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Customer)
                .WithMany(cu => cu.Bills)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bills)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Counter)
                .WithMany(c => c.Bills)
                .HasForeignKey(b => b.CounterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BillJewelry>()
                .HasOne(bj => bj.Bill)
                .WithMany(b => b.BillJewelries)
                .HasForeignKey(bj => bj.BillId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BillJewelry>()
                .HasOne(bj => bj.Jewelry)
                .WithMany(j => j.BillJewelries)
                .HasForeignKey(bj => bj.JewelryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BillPromotion>()
                .HasOne(bp => bp.Bill)
                .WithMany(b => b.BillPromotions)
                .HasForeignKey(bp => bp.BillId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BillPromotion>()
                .HasOne(bp => bp.Promotion)
                .WithMany(p => p.BillPromotions)
                .HasForeignKey(bp => bp.PromotionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Jewelry>()
                .HasOne(j => j.JewelryType)
                .WithMany(jt => jt.Jewelries)
                .HasForeignKey(j => j.JewelryTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Customer)
                .WithMany(cu => cu.Purchases)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Jewelry)
                .WithMany(j => j.Purchases)
                .HasForeignKey(p => p.JewelryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Counter)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CounterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Payment)
                .WithOne(p => p.Bill)
                .HasForeignKey<Payment>(p => p.BillId);
            // Seed data

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = "1", RoleName = "Admin" },
                new Role { RoleId = "2", RoleName = "Manager" },
                new Role { RoleId = "3", RoleName = "Staff" }
            );

            modelBuilder.Entity<Counter>().HasData(
                new Counter { CounterId = "1", Number = 312 },
                new Counter { CounterId = "2", Number = 231 },
                new Counter { CounterId = "3", Number = 431 }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = "1", Username = "admin Nghia", Password = "5678", Email = "nghialoe46a2gmail.com",
                    RoleId = "1", CounterId = "1"
                },
                new User
                {
                    UserId = "2", Username = "manager John Doe", Password = "1234", Email = "JohnDoe@gmail.com",
                    RoleId = "2", CounterId = "2"
                },
                new User
                {
                    UserId = "3", Username = "staff Chis Nguyen", Password = "4321", Email = "Chis@yahho.com",
                    RoleId = "3", CounterId = "3"
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = "1", FullName = "Nguyen Van A", Phone = "0123456789", Address = "Ha Noi" },
                new Customer { CustomerId = "2", FullName = "Nguyen Van B", Phone = "0123456789", Address = "Ha Noi" },
                new Customer { CustomerId = "3", FullName = "Nguyen Van C", Phone = "0123456789", Address = "Ha Noi" }
            );

            modelBuilder.Entity<JewelryType>().HasData(
                new JewelryType { JewelryTypeId = "1", Name = "Vong tay" },
                new JewelryType { JewelryTypeId = "2", Name = "Nhan" },
                new JewelryType { JewelryTypeId = "3", Name = "Day chuyen" }
            );

            modelBuilder.Entity<Jewelry>().HasData(
                new Jewelry
                {
                    JewelryId = "1", Name = "Vong tay", JewelryTypeId = "1", Barcode = "AVC131", LaborCost = 312,
                    IsSold = true
                },
                new Jewelry
                {
                    JewelryId = "2", Name = "Nhan", JewelryTypeId = "2", Barcode = "SAC132", LaborCost = 231,
                    IsSold = false
                }
            );
            modelBuilder.Entity<JewelryMaterial>().HasData(
                new JewelryMaterial
                {
                    JewelryMaterialId = "1", JewelryId = "1", GoldQuantity = 30, GoldPriceId = "1", StoneQuantity = 1,
                    StonePriceId = "1"
                },
                new JewelryMaterial
                {
                    JewelryMaterialId = "2", JewelryId = "2", GoldQuantity = 20, GoldPriceId = "2", StoneQuantity = 1,
                    StonePriceId = "2"
                }
            );
            modelBuilder.Entity<Promotion>().HasData(
                new Promotion
                {
                    PromotionId = "1", Type = "Giam gia", Description = "Giam gia 10%", DiscountRate = 1,
                    StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10)
                },
                new Promotion
                {
                    PromotionId = "2", Type = "Giam gia", Description = "Giam gia 20%", DiscountRate = 2,
                    StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10)
                },
                new Promotion
                {
                    PromotionId = "3", Type = "Giam gia", Description = "Giam gia 30%", DiscountRate = 3,
                    StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10)
                }
            );

            modelBuilder.Entity<Bill>().HasData(
                new Bill
                {
                    CounterId = "1", BillId = "1", CustomerId = "1", UserId = "1", SaleDate = DateTime.Now,
                    TotalAmount = 500
                },
                new Bill
                {
                    CounterId = "1", BillId = "2", CustomerId = "2", UserId = "2", SaleDate = DateTime.Now,
                    TotalAmount = 1200
                }
            );

            modelBuilder.Entity<BillJewelry>().HasData(
                new BillJewelry { BillJewelryId = "1", BillId = "1", JewelryId = "1" },
                new BillJewelry { BillJewelryId = "2", BillId = "1", JewelryId = "2" }
            );

            modelBuilder.Entity<BillPromotion>().HasData(
                new BillPromotion { BillPromotionId = "1", BillId = "1", PromotionId = "1" },
                new BillPromotion { BillPromotionId = "2", BillId = "2", PromotionId = "1" }
            );

            modelBuilder.Entity<Gem>().HasData(
                new Gem
                {
                    GemId = "1", BuyPrice = 300, SellPrice = 400, LastUpdated = DateTime.Now, Type = "Ruby",
                    City = "Ha Noi"
                },
                new Gem
                {
                    GemId = "2", BuyPrice = 400, SellPrice = 500, LastUpdated = DateTime.Now, Type = "Sapphire",
                    City = "Ha Noi"
                },
                new Gem
                {
                    GemId = "3", BuyPrice = 500, SellPrice = 600, LastUpdated = DateTime.Now, Type = "Emerald",
                    City = "Ha Noi"
                }
            );

            modelBuilder.Entity<Gold>().HasData(
                new Gold
                {
                    GoldId = "1", BuyPrice = 1000, SellPrice = 1200, LastUpdated = DateTime.Now, City = "Ha Noi",
                    Type = "9999"
                },
                new Gold
                {
                    GoldId = "2", BuyPrice = 1200, SellPrice = 1400, LastUpdated = DateTime.Now, City = "Ha Noi",
                    Type = "SCJ"
                },
                new Gold
                {
                    GoldId = "3", BuyPrice = 1400, SellPrice = 1600, LastUpdated = DateTime.Now, City = "Ha Noi",
                    Type = "18k"
                }
            );

            modelBuilder.Entity<Purchase>().HasData(
                new Purchase
                {
                    PurchaseId = "1", CustomerId = "1", JewelryId = "1", UserId = "1", IsBuyBack = 0,
                    PurchasePrice = 500,
                    PurchaseDate = DateTime.Now
                },
                new Purchase
                {
                    PurchaseId = "2", CustomerId = "2", JewelryId = "2", UserId = "1", IsBuyBack = 1,
                    PurchasePrice = 300,
                    PurchaseDate = DateTime.Now
                }
            );
        }
    }
}