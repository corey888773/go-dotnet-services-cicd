using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Data;

public class NumbersConfiguration : IEntityTypeConfiguration<RandomNumberRecord>
{
    public void Configure(EntityTypeBuilder<RandomNumberRecord> builder)
    {
        builder.ToTable("numbers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Number).HasColumnName("number");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");

        builder.HasIndex(x => x.CreatedAt);

    }
}