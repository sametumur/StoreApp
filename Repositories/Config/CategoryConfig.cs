using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
        builder.HasData(
            new Category() { Id = 1, Name = "Electronics" },
            new Category() { Id = 2, Name = "Books" },
            new Category() { Id = 3, Name = "Clothes" },
            new Category() { Id = 4, Name = "Phones" }
        );
    }
}