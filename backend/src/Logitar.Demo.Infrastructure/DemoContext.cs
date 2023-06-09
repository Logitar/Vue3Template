using Logitar.Demo.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Logitar.Demo.Infrastructure;

public class DemoContext : DbContext
{
  public DemoContext(DbContextOptions<DemoContext> options) : base(options)
  {
  }

  internal DbSet<ActorEntity> Actors { get; private set; } = null!;
  internal DbSet<RealmEntity> Realms { get; private set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoContext).Assembly);
  }
}
