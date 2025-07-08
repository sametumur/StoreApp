using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;
using Entities.Models;
using Microsoft.EntityFrameworkCore;



public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.HasData(
            new Product() { Id = 1, Name = "Computer", ImageUrl = "/images/1.webp",CategoryId = 1, Price = 17_000, Showcase = false},
            new Product() { Id = 2, Name = "Keyboard", ImageUrl = "/images/2.webp", CategoryId = 1, Price = 1_000, Showcase = false},
            new Product() { Id = 3, Name = "Mouse", ImageUrl = "/images/3.webp", CategoryId = 1, Price = 500, Showcase = false},
            new Product() { Id = 4, Name = "Monitor", ImageUrl = "/images/4.webp", CategoryId = 1, Price = 7_000, Showcase = true},
            new Product() { Id = 5, Name = "Deck", ImageUrl = "/images/5.webp", CategoryId = 1, Price = 1500, Showcase = false},
            new Product() { Id = 6, Name = "Microphone", ImageUrl = "/images/6.webp", CategoryId = 1, Price = 900, Showcase = false},
            new Product() { Id = 7, Name = "History Book", ImageUrl = "/images/7.webp", CategoryId = 2, Price = 800, Showcase = false},
            new Product() { Id = 8, Name = "Englist Book", ImageUrl = "/images/8.webp", CategoryId = 2, Price = 900, Showcase = false},
            new Product() { Id = 9, Name = "T-shirt", ImageUrl = "/images/9.webp", CategoryId = 3, Price = 600, Showcase = false},
            new Product() { Id = 10, Name = "Jeans", ImageUrl = "/images/10.webp", CategoryId = 3, Price = 750, Showcase = false},
            new Product() { Id = 11, Name = "Iphone 14", ImageUrl = "/images/11.webp", CategoryId = 4, Price = 33500, Showcase = true},
            new Product() { Id = 12, Name = "Samsung", ImageUrl = "/images/12.webp", CategoryId = 4, Price = 27500, Showcase = true}
        );
    }
}