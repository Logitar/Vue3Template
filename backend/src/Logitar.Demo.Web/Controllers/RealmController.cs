using Logitar.Demo.Core;
using Logitar.Demo.Core.Realms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Demo.Web.Controllers;

[ApiController]
[Authorize]
[Route("realms")]
public class RealmController : ControllerBase
{
  private readonly IRealmService _realmService;

  public RealmController(IRealmService realmService)
  {
    _realmService = realmService;
  }

  [HttpPost]
  public async Task<ActionResult<Realm>> CreateAsync([FromBody] CreateRealmInput input, CancellationToken cancellationToken)
  {
    Realm realm = await _realmService.CreateAsync(input, cancellationToken);
    Uri uri = new($"{Request.Scheme}://{Request.Host}/realms/{realm.Id}");

    return Created(uri, realm);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<Realm>> DeleteAsync(Guid id, CancellationToken cancellationToken)
  {
    return Ok(await _realmService.DeleteAsync(id, cancellationToken));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Realm>> GetAsync(Guid id, CancellationToken cancellationToken)
  {
    Realm? realm = await _realmService.GetAsync(id, cancellationToken: cancellationToken);
    if (realm == null)
    {
      return NotFound();
    }

    return Ok(realm);
  }

  /// <summary>
  /// TODO(fpion): refactor, FromQuery
  /// </summary>
  /// <param name="parameters"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpPost("search")]
  public async Task<ActionResult<SearchResults<Realm>>> SearchAsync([FromBody] RealmSearchParameters parameters, CancellationToken cancellationToken)
  {
    return Ok(await _realmService.SearchAsync(parameters, cancellationToken));
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Realm>> UpdateAsync(Guid id, [FromBody] UpdateRealmInput input, CancellationToken cancellationToken)
  {
    return Ok(await _realmService.UpdateAsync(id, input, cancellationToken));
  }
}
