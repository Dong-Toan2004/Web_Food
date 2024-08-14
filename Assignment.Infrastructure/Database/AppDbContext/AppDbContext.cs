using Assignment.Domain.Database.Base;
using Assignment.Domain.Database.Entities;
using Assignment.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Database.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboDetail> ComboDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cartdetail> Cartdetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-JMN439Q3\\SQLEXPRESS02;Initial Catalog=Web_Food;Integrated Security=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var dataCategory = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.Parse("bbec7cc9-19f0-49ba-8f44-4afe06213e2a"),
                    Name = "Gà rán"
                },
                new Category()
                {
                    Id= Guid.Parse("cfcb9512-6d77-41a2-979e-f2643664c5ef"),
                    Name = "Pizza"
                },
                new Category()
                {
                    Id = Guid.Parse("aec4d6dc-61c2-40d0-8663-2af87aa18812"),
                    Name = "Bánh mì"
                }
            };
            modelBuilder.Entity<Category>().HasData(dataCategory);

            var product = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Gà rán",
                    Price = 50000,
                    Image = "A:\\NET106-C#6\\Assignment\\Assignment.API\\UpLoadImage\\anh-ga-ran2.jpg",
                    Description = "Gà rán cay",
                    CategoryId = Guid.Parse("bbec7cc9-19f0-49ba-8f44-4afe06213e2a"),
                    QRCode = ""
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Đùi gà rán",
                    Price = 50000,
                    Image = "A:\\NET106-C#6\\Assignment\\Assignment.API\\UpLoadImage\\anh-ga-ran.jpg",
                    Description = "Gà rán siêu ngon",
                    CategoryId = Guid.Parse("bbec7cc9-19f0-49ba-8f44-4afe06213e2a"),
                    QRCode = ""
                }
            };

            modelBuilder.Entity<Product>().HasData(product);
            //Hỗ trợ seeddata
            var salt = PasswordHelper.CreateSalt();
            var passwordhash = PasswordHelper.CreatePasswordHash("123", salt);

            var dataUser = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = "Admin1",
                    PasswordHash = passwordhash,
                    PasswordSalt = salt,
                    Email = "admin1@gmail.com",
                    PhoneNumber = "0123456789",
                    Address = "Hà Nội",
                    Role = Role.Admin,
                }
            };
            modelBuilder.Entity<User>().HasData(dataUser);
        }
    }
}
