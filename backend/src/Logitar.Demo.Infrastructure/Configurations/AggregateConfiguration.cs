using Logitar.Demo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logitar.Demo.Infrastructure.Configurations;

internal abstract class AggregateConfiguration<T> where T : AggregateEntity
{
  public virtual void Configure(EntityTypeBuilder<T> builder)
  {
    builder.HasIndex(x => x.AggregateId).IsUnique();
    builder.HasIndex(x => x.CreatedById);
    builder.HasIndex(x => x.CreatedOn);
    builder.HasIndex(x => x.UpdatedById);
    builder.HasIndex(x => x.UpdatedOn);

    builder.Property(x => x.AggregateId).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.CreatedBy).HasColumnType("jsonb");
    builder.Property(x => x.UpdatedBy).HasColumnType("jsonb");
  }
}
