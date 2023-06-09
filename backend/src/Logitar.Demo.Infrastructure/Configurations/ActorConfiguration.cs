using Logitar.Demo.Core.Actors;
using Logitar.Demo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Logitar.Demo.Infrastructure.Configurations;

internal class ActorConfiguration : IEntityTypeConfiguration<ActorEntity>
{
  public void Configure(EntityTypeBuilder<ActorEntity> builder)
  {
    builder.HasKey(x => x.ActorId);

    builder.HasIndex(x => x.Id).IsUnique();

    builder.Property(x => x.Type).HasMaxLength(byte.MaxValue).HasConversion(new EnumToStringConverter<ActorType>());
    builder.Property(x => x.DisplayName).HasMaxLength(ushort.MaxValue);
    builder.Property(x => x.EmailAddress).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.Picture).HasMaxLength(ushort.MaxValue);
  }
}
