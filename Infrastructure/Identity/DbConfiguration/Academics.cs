using Domain.Entities;
using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.DbConfiguration;

internal class SchoolConfig : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder
            .ToTable("Schools", SchemaNames.Academics)
            .IsMultiTenant();
        
        builder
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(60);
    }   
}