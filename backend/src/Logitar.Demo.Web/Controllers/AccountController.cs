using Logitar.Demo.Web.Extensions;
using Logitar.Demo.Web.Models.Account;
using Logitar.Portal.Contracts.Errors;
using Logitar.Portal.Contracts.Messages;
using Logitar.Portal.Contracts.Sessions;
using Logitar.Portal.Contracts.Tokens;
using Logitar.Portal.Contracts.Users;
using Logitar.Portal.Contracts.Users.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logitar.Demo.Web.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
  private const string AccountConfirmationPurpose = "verify_email";

  private readonly string _realm;

  private readonly ILogger<AccountController> _logger;
  private readonly IMessageService _messageService;
  private readonly ISessionService _sessionService;
  private readonly ITokenService _tokenService;
  private readonly IUserService _userService;

  public AccountController(IConfiguration configuration,
    ILogger<AccountController> logger,
    IMessageService messageService,
    ISessionService sessionService,
    ITokenService tokenService,
    IUserService userService)
  {
    _realm = configuration.GetSection("Portal").GetValue<string>("Realm")
      ?? throw new InvalidOperationException("The configuration key 'Portal:Realm' has not been set.");

    _logger = logger;
    _messageService = messageService;
    _sessionService = sessionService;
    _tokenService = tokenService;
    _userService = userService;
  }

  [HttpPost("confirm")]
  public async Task<ActionResult> ConfirmAsync([FromBody] ConfirmPayload payload, CancellationToken cancellationToken)
  {
    ValidateTokenInput validateToken = new()
    {
      Token = payload.Token,
      Purpose = AccountConfirmationPurpose,
      Realm = _realm
    };
    ValidatedToken validatedToken = await _tokenService.ConsumeAsync(validateToken, cancellationToken);
    if (!validatedToken.Succeeded || !Guid.TryParse(validatedToken.Subject, out Guid userId))
    {
      return InvalidCredentials();
    }

    _ = await _userService.VerifyEmailAsync(userId, cancellationToken);

    return NoContent();
  }

  [HttpPost("password/recover")]
  public async Task<ActionResult> RecoverPasswordAsync([FromBody] RecoverPasswordPayload payload, CancellationToken cancellationToken)
  {
    try
    {
      RecoverPasswordInput input = new()
      {
        Realm = _realm,
        Username = payload.Username
      };
      SentMessages sentMessages = await _userService.RecoverPasswordAsync(input, cancellationToken);
      if (sentMessages.Success.Count() != 1)
      {
        _logger.LogWarning("User '{userId}' password recovery has multiple sent messages: {success}", payload.Username, string.Join(", ", sentMessages.Success));
      }
      if (sentMessages.Error.Any())
      {
        _logger.LogError("User '{userId}' password recovery has error messages: {error}", payload.Username, string.Join(", ", sentMessages.Error));
      }
      if (sentMessages.Unsent.Any())
      {
        _logger.LogWarning("User '{userId}' password recovery has unsent messages: {unsent}", payload.Username, string.Join(", ", sentMessages.Unsent));
      }
    }
    catch (ErrorException exception)
    {
      ErrorData? statusCode = exception.Error.Data.SingleOrDefault(d => d.Key == "StatusCode");
      ErrorData? content = exception.Error.Data.SingleOrDefault(d => d.Key == "Content");
      if (statusCode?.Value != StatusCodes.Status400BadRequest.ToString()
        || (content?.Value.Contains("AccountIsDisabled") != true
          && content?.Value.Contains("AccountIsNotConfirmed") != true
          && content?.Value.Contains("InvalidCredentials") != true))
      {
        throw;
      }
    }

    return NoContent();
  }

  [HttpGet("password/reset")]
  public async Task<ActionResult> ResetPasswordAsync(string token, CancellationToken cancellationToken)
  {
    ValidateTokenInput input = new()
    {
      Token = token,
      Realm = _realm,
      Purpose = "reset_password"
    };
    ValidatedToken validatedToken = await _tokenService.ValidateAsync(input, cancellationToken);
    if (!validatedToken.Succeeded)
    {
      return InvalidCredentials();
    }

    return NoContent();
  }

  [HttpPost("password/reset")]
  public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordPayload payload, CancellationToken cancellationToken)
  {
    ResetPasswordInput input = new()
    {
      Token = payload.Token,
      Realm = _realm,
      Password = payload.Password
    };
    _ = await _userService.ResetPasswordAsync(input, cancellationToken);

    return NoContent();
  }

  [HttpPost("register")]
  public async Task<ActionResult> RegisterAsync([FromBody] RegisterPayload payload, CancellationToken cancellationToken)
  {
    try
    {
      CreateUserInput createUser = new()
      {
        Realm = _realm,
        Username = payload.Username,
        Password = payload.Password,
        Email = new EmailInput
        {
          Address = payload.EmailAddress
        },
        FirstName = payload.FirstName,
        LastName = payload.LastName,
        Locale = payload.Locale
      };
      User user = await _userService.CreateAsync(createUser, cancellationToken);

      CreateTokenInput createToken = new()
      {
        IsConsumable = true,
        Lifetime = 7 * 24 * 60 * 60, // 7 days
        Purpose = AccountConfirmationPurpose,
        Realm = _realm,
        Subject = user.Id.ToString()
      };
      CreatedToken createdToken = await _tokenService.CreateAsync(createToken, cancellationToken);

      SendMessageInput sendMessage = new()
      {
        Realm = _realm,
        Template = "AccountConfirmation",
        Recipients = new RecipientInput[]
        {
          new()
          {
            User = user.Id.ToString()
          }
        },
        Variables = new Variable[]
        {
          new()
          {
            Key = "Token",
            Value = createdToken.Token
          }
        }
      };
      SentMessages sentMessages = await _messageService.SendAsync(sendMessage, cancellationToken);
      if (sentMessages.Success.Count() != 1)
      {
        _logger.LogWarning("User '{userId}' registration has multiple sent messages: {success}", user.Id, string.Join(", ", sentMessages.Success));
      }
      if (sentMessages.Error.Any())
      {
        _logger.LogError("User '{userId}' registration has error messages: {error}", user.Id, string.Join(", ", sentMessages.Error));
      }
      if (sentMessages.Unsent.Any())
      {
        _logger.LogWarning("User '{userId}' registration has unsent messages: {unsent}", user.Id, string.Join(", ", sentMessages.Unsent));
      }
    }
    catch (ErrorException exception)
    {
      ErrorData? statusCode = exception.Error.Data.SingleOrDefault(d => d.Key == "StatusCode");
      ErrorData? content = exception.Error.Data.SingleOrDefault(d => d.Key == "Content");
      if (statusCode?.Value != StatusCodes.Status409Conflict.ToString()
        || (content?.Value.Contains("EmailAddressAlreadyUsed") != true && content?.Value.Contains("UniqueNameAlreadyUsed") != true))
      {
        throw;
      }
    }

    return NoContent();
  }

  [HttpPost("sign/in")]
  public async Task<ActionResult> SignInAsync([FromBody] SignInPayload payload, CancellationToken cancellationToken)
  {
    try
    {
      SignInInput input = new()
      {
        Username = payload.Username,
        Password = payload.Password,
        Remember = payload.Remember,
        IpAddress = HttpContext.GetClientIpAddress(),
        AdditionalInformation = HttpContext.GetAdditionalInformation()
      };
      Session session = await _sessionService.SignInAsync(input, _realm, cancellationToken);
      HttpContext.SignIn(session);

      return NoContent();
    }
    catch (ErrorException exception)
    {
      ErrorData? statusCode = exception.Error.Data.SingleOrDefault(d => d.Key == "StatusCode");
      ErrorData? content = exception.Error.Data.SingleOrDefault(d => d.Key == "Content");
      if (statusCode?.Value == StatusCodes.Status400BadRequest.ToString()
        && (content?.Value.Contains("AccountIsDisabled") == true
          || content?.Value.Contains("AccountIsNotConfirmed") == true
          || content?.Value.Contains("InvalidCredentials") == true))
      {
        return InvalidCredentials();
      }

      throw;
    }
  }

  [Authorize]
  [HttpPost("sign/out")]
  public async Task<ActionResult> SignOutAsync(CancellationToken cancellationToken)
  {
    Guid? sessionId = HttpContext.GetSessionId();

    if (sessionId.HasValue)
    {
      _ = await _sessionService.SignOutAsync(sessionId.Value, cancellationToken);
      HttpContext.SignOut();
    }

    return NoContent();
  }

  private ActionResult InvalidCredentials() => BadRequest(new { Code = "InvalidCredentials" });
}
