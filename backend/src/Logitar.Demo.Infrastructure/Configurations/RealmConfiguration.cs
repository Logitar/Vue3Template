using Logitar.Demo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logitar.Demo.Infrastructure.Configurations;

internal class RealmConfiguration : AggregateConfiguration<RealmEntity>, IEntityTypeConfiguration<RealmEntity>
{
  public override void Configure(EntityTypeBuilder<RealmEntity> builder)
  {
    base.Configure(builder);

    builder.HasKey(x => x.RealmId);

    builder.HasIndex(x => x.UniqueName);
    builder.HasIndex(x => x.UniqueNameNormalized).IsUnique();
    builder.HasIndex(x => x.DisplayName);

    builder.Property(x => x.UniqueName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.UniqueNameNormalized).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.DisplayName).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.DefaultLocale).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.Secret).HasMaxLength(byte.MaxValue);
    builder.Property(x => x.Url).HasMaxLength(ushort.MaxValue);
    builder.Property(x => x.UsernameSettings).HasColumnType("jsonb");
    builder.Property(x => x.PasswordSettings).HasColumnType("jsonb");
    builder.Property(x => x.ClaimMappings).HasColumnType("jsonb");
    builder.Property(x => x.CustomAttributes).HasColumnType("jsonb");
  }
}
