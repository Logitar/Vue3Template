using AutoMapper;
using Logitar.Demo.Core;
using Logitar.Demo.Core.Realms;
using Logitar.Demo.Infrastructure.Entities;
using Logitar.EventSourcing;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text;

namespace Logitar.Demo.Infrastructure.Queriers;

internal class RealmQuerier : IRealmQuerier
{
  private readonly IMapper _mapper;
  private readonly DbSet<RealmEntity> _realms;

  public RealmQuerier(DemoContext context, IMapper mapper)
  {
    _mapper = mapper;
    _realms = context.Realms;
  }

  public async Task<Realm> GetAsync(RealmAggregate realm, CancellationToken cancellationToken)
  {
    RealmEntity entity = await _realms.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == realm.Id.Value, cancellationToken)
      ?? throw new InvalidOperationException($"The realm entity '{realm.Id}' could not be found.");

    return _mapper.Map<Realm>(entity);
  }

  public async Task<Realm?> GetAsync(Guid id, CancellationToken cancellationToken)
  {
    string aggregateId = new AggregateId(id).Value;

    RealmEntity? realm = await _realms.AsNoTracking()
      .SingleOrDefaultAsync(x => x.AggregateId == aggregateId, cancellationToken);

    return _mapper.Map<Realm>(realm);
  }

  public async Task<Realm?> GetAsync(string uniqueName, CancellationToken cancellationToken)
  {
    RealmEntity? realm = await _realms.AsNoTracking()
      .SingleOrDefaultAsync(x => x.UniqueNameNormalized == uniqueName.ToUpper(), cancellationToken);

    return _mapper.Map<Realm>(realm);
  }

  public async Task<SearchResults<Realm>> SearchAsync(RealmSearchParameters parameters, CancellationToken cancellationToken)
  {
    List<string> conditions = new(capacity: 1);
    List<object> sqlParameters = new();

    if (parameters.Search.Terms.Any() && Enum.IsDefined(parameters.Search.Operator))
    {
      List<string> parts = new();

      int index = 0;
      foreach (SearchTerm term in parameters.Search.Terms)
      {
        if (!string.IsNullOrWhiteSpace(term.Value))
        {
          string parameterName = $"term{index}";
          sqlParameters.Add(new NpgsqlParameter(parameterName, term.Value));

          parts.Add($@"(""UniqueName"" ILIKE @{parameterName} OR ""DisplayName"" ILIKE @{parameterName})");

          index++;
        }
      }

      if (parts.Any())
      {
        conditions.Add(string.Join($" {parameters.Search.Operator.ToString().ToUpper()} ", parts));
      }
    }

    StringBuilder sql = new(@"SELECT * FROM ""Realms""");
    if (conditions.Any())
    {
      sql.AppendFormat(" WHERE {0}", string.Join(" AND", conditions));
    }
    IQueryable<RealmEntity> query = _realms.FromSqlRaw(sql.ToString(), sqlParameters.ToArray());

    long total = await query.LongCountAsync(cancellationToken);

    if (parameters.Sort.Any())
    {
      IOrderedQueryable<RealmEntity>? ordered = null;

      foreach (RealmSortParameters sort in parameters.Sort)
      {
        switch (sort.Field)
        {
          case RealmSort.DisplayName:
            if (ordered == null)
            {
              ordered = sort.IsDescending ? query.OrderByDescending(x => x.DisplayName) : query.OrderBy(x => x.DisplayName);
            }
            else
            {
              ordered = sort.IsDescending ? ordered.ThenByDescending(x => x.DisplayName) : ordered.ThenBy(x => x.DisplayName);
            }
            break;
          case RealmSort.UniqueName:
            if (ordered == null)
            {
              ordered = sort.IsDescending ? query.OrderByDescending(x => x.UniqueName) : query.OrderBy(x => x.UniqueName);
            }
            else
            {
              ordered = sort.IsDescending ? ordered.ThenByDescending(x => x.UniqueName) : ordered.ThenBy(x => x.UniqueName);
            }
            break;
          case RealmSort.UpdatedOn:
            if (ordered == null)
            {
              ordered = sort.IsDescending ? query.OrderByDescending(x => x.UpdatedOn ?? x.CreatedOn) : query.OrderBy(x => x.UpdatedOn ?? x.CreatedOn);
            }
            else
            {
              ordered = sort.IsDescending ? ordered.ThenByDescending(x => x.UpdatedOn ?? x.CreatedOn) : ordered.ThenBy(x => x.UpdatedOn ?? x.CreatedOn);
            }
            break;
        }
      }

      query = ordered ?? query;
    }

    if (parameters.Skip > 0)
    {
      query = query.Skip(parameters.Skip);
    }
    if (parameters.Limit > 0)
    {
      query = query.Take(parameters.Limit);
    }

    RealmEntity[] results = await query.ToArrayAsync(cancellationToken);

    return new SearchResults<Realm>
    {
      Results = _mapper.Map<IEnumerable<Realm>>(results),
      Total = total
    };
  }
}
