using Microsoft.EntityFrameworkCore;
using BubbleTeaCafe.Models;

namespace BubbleTeaCafe.Data;

public class BubbleTeaCafeContext : DbContext
{
    public BubbleTeaCafeContext(DbContextOptions<BubbleTeaCafeContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure relationships
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
            
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
            
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
        
        // Seed data
        SeedData(modelBuilder);
    }
    
    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Trà Sữa", Description = "Bubble Tea với nhiều hương vị thơm ngon" },
            new Category { CategoryId = 2, Name = "Cà Phê", Description = "Cà phê rang xay thơm ngon" },
            new Category { CategoryId = 3, Name = "Trà Trái Cây", Description = "Trà trái cây tươi mát" },
            new Category { CategoryId = 4, Name = "Smoothie", Description = "Sinh tố trái cây tươi ngon" }
        );
        
        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            // Bubble Tea
            new Product 
            { 
                ProductId = 1, 
                Name = "Trà Sữa Truyền Thống", 
                Description = "Trà sữa đậm đà với trân châu đen", 
                Price = 35000, 
                CategoryId = 1, 
                ImageUrl = "/images/tra-sua-truyen-thong.svg",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Hot",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Trà đen, sữa tươi, trân châu đen"
            },
            new Product 
            { 
                ProductId = 2, 
                Name = "Trà Sữa Matcha", 
                Description = "Trà sữa matcha Nhật Bản thơm ngon", 
                Price = 40000, 
                CategoryId = 1, 
                ImageUrl = "/images/tra-sua-matcha.svg",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Hot",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Matcha Nhật Bản, sữa tươi, trân châu trắng"
            },
            new Product 
            { 
                ProductId = 3, 
                Name = "Trà Sữa Taro", 
                Description = "Trà sữa khoai môn tím thơm béo", 
                Price = 38000, 
                CategoryId = 1, 
                ImageUrl = "https://images.unsplash.com/photo-1571934811356-5cc061b6821f?w=400&h=400&fit=crop&crop=center",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Hot",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Khoai môn tím, sữa tươi, trân châu đen"
            },
            
            // Coffee
            new Product 
            { 
                ProductId = 4, 
                Name = "Cà Phê Đen Đá", 
                Description = "Cà phê đen truyền thống Việt Nam", 
                Price = 25000, 
                CategoryId = 2, 
                ImageUrl = "/images/ca-phe-den-da.svg",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Hot",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Cà phê robusta rang xay"
            },
            new Product 
            { 
                ProductId = 5, 
                Name = "Cà Phê Sữa Đá", 
                Description = "Cà phê sữa đá đậm đà", 
                Price = 30000, 
                CategoryId = 2, 
                ImageUrl = "/images/ca-phe-sua-da.svg",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Hot",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Cà phê robusta, sữa đặc"
            },
            new Product 
            { 
                ProductId = 6, 
                Name = "Cappuccino", 
                Description = "Cappuccino Ý thơm ngon", 
                Price = 45000, 
                CategoryId = 2, 
                ImageUrl = "https://images.unsplash.com/photo-1534778101976-62847782c213?w=400&h=400&fit=crop&crop=center",
                SizeOptions = "M,L",
                TemperatureOptions = "Hot",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Espresso, sữa tươi, bọt sữa"
            },
            
            // Fruit Tea
            new Product 
            { 
                ProductId = 7, 
                Name = "Trà Đào", 
                Description = "Trà đào tươi mát", 
                Price = 32000, 
                CategoryId = 3, 
                ImageUrl = "/images/tra-dao.svg",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Iced",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Trà xanh, đào tươi, mật ong"
            },
            new Product 
            { 
                ProductId = 8, 
                Name = "Trà Chanh", 
                Description = "Trà chanh chua ngọt", 
                Price = 28000, 
                CategoryId = 3, 
                ImageUrl = "https://images.unsplash.com/photo-1556679343-c7306c1976bc?w=400&h=400&fit=crop&crop=center",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold,Iced",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Trà đen, chanh tươi, đường"
            },
            
            // Smoothies
            new Product 
            { 
                ProductId = 9, 
                Name = "Sinh Tố Xoài", 
                Description = "Sinh tố xoài tươi ngon", 
                Price = 42000, 
                CategoryId = 4, 
                ImageUrl = "/images/sinh-to-xoai.svg",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Xoài tươi, sữa tươi, đá"
            },
            new Product 
            { 
                ProductId = 10, 
                Name = "Sinh Tố Dâu", 
                Description = "Sinh tố dâu tây tươi mát", 
                Price = 45000, 
                CategoryId = 4, 
                ImageUrl = "https://images.unsplash.com/photo-1553530666-ba11a7da3888?w=400&h=400&fit=crop&crop=center",
                SizeOptions = "M,L",
                TemperatureOptions = "Cold",
                SweetnessOptions = "0%,25%,50%,75%,100%",
                Ingredients = "Dâu tây tươi, sữa tươi, đá"
            }
        );
    }
}
