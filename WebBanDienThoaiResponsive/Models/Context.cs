using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebBanDienThoaiResponsive.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<AdminConfig> AdminConfigs { get; set; }
        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<AuthenticationQAndA> AuthenticationQAndAs { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<BrandAdvertisement> BrandAdvertisements { get; set; }
        public virtual DbSet<CarouselSlider> CarouselSliders { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<MemberAccount> MemberAccounts { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProblemContact> ProblemContacts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<QAndA> QAndAs { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<WebInfo> WebInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountType>()
                .Property(e => e.Discount)
                .HasPrecision(5, 0);

            modelBuilder.Entity<AccountType>()
                .HasMany(e => e.MemberAccounts)
                .WithOptional(e => e.AccountType)
                .HasForeignKey(e => e.MemberTypeID);

            modelBuilder.Entity<AdminConfig>()
                .Property(e => e.AdEmail)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AdminConfig>()
                .Property(e => e.AdPassword)
                .IsUnicode(false);

            modelBuilder.Entity<AdminConfig>()
                .Property(e => e.AdPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.BeginDate)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.LinkTo)
                .IsUnicode(false);

            modelBuilder.Entity<Advertisement>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<AuthenticationQAndA>()
                .HasMany(e => e.MemberAccounts)
                .WithRequired(e => e.AuthenticationQAndA)
                .HasForeignKey(e => e.IDQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Banner>()
                .Property(e => e.LinkTo)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .Property(e => e.LogoURL)
                .IsUnicode(false);

            modelBuilder.Entity<BrandAdvertisement>()
                .Property(e => e.URLTo)
                .IsUnicode(false);

            modelBuilder.Entity<CarouselSlider>()
                .Property(e => e.UrlTo)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.CommentDate)
                .IsUnicode(false);

            modelBuilder.Entity<MemberAccount>()
                .Property(e => e.Email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MemberAccount>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<MemberAccount>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<MemberAccount>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.MemberAccount)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<MemberAccount>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.MemberAccount)
                .HasForeignKey(e => e.MemberID);

            modelBuilder.Entity<News>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.PostDate)
                .IsUnicode(false);

            modelBuilder.Entity<News>()
                .Property(e => e.LinkTo)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Discount)
                .HasPrecision(5, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.TransferID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.PriceNow)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProblemContact>()
                .HasMany(e => e.QAndAs)
                .WithRequired(e => e.ProblemContact)
                .HasForeignKey(e => e.IDProblem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Discount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QAndA>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<QAndA>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);
        }
    }
}
