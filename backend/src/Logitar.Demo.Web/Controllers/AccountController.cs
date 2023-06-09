﻿using AutoMapper;
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
  private readonly IMapper _mapper;
  private readonly IMessageService _messageService;
  private readonly ISessionService _sessionService;
  private readonly ITokenService _tokenService;
  private readonly IUserService _userService;

  public AccountController(IConfiguration configuration,
    ILogger<AccountController> logger,
    IMapper mapper,
    IMessageService messageService,
    ISessionService sessionService,
    ITokenService tokenService,
    IUserService userService)
  {
    _realm = configuration.GetSection("Portal").GetValue<string>("Realm")
      ?? throw new InvalidOperationException("The configuration key 'Portal:Realm' has not been set.");

    _logger = logger;
    _mapper = mapper;
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

  [Authorize]
  [HttpPost("password/change")]
  public async Task<ActionResult<UserProfile>> ChangePasswordAsync([FromBody] ChangePasswordInput input, CancellationToken cancellationToken)
  {
    try
    {
      User user = HttpContext.GetUser() ?? throw new InvalidOperationException("The User is required.");
      user = await _userService.ChangePasswordAsync(user.Id, input, cancellationToken);

      return Ok(_mapper.Map<UserProfile>(user));
    }
    catch (ErrorException exception)
    {
      ErrorData? statusCode = exception.Error.Data.SingleOrDefault(d => d.Key == "StatusCode");
      ErrorData? content = exception.Error.Data.SingleOrDefault(d => d.Key == "Content");
      if (statusCode?.Value == StatusCodes.Status400BadRequest.ToString() && content?.Value.Contains("InvalidCredentials") == true)
      {
        return InvalidCredentials();
      }

      throw;
    }
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

  [Authorize]
  [HttpGet("profile")]
  public ActionResult<UserProfile> GetProfile()
  {
    User user = HttpContext.GetUser() ?? throw new InvalidOperationException("The User is required.");

    return Ok(_mapper.Map<UserProfile>(user));
  }

  [Authorize]
  [HttpPut("profile/contact")]
  public async Task<ActionResult<UserProfile>> SaveContactInformationAsync([FromBody] SaveContactInformationPayload payload, CancellationToken cancellationToken)
  {
    User user = HttpContext.GetUser() ?? throw new InvalidOperationException("The User is required.");

    UpdateUserInput input = new()
    {
      Address = payload.Address,
      Email = payload.Email,
      Phone = payload.Phone,
      FirstName = user.FirstName,
      MiddleName = user.MiddleName,
      LastName = user.LastName,
      Nickname = user.Nickname,
      Birthdate = user.Birthdate,
      Gender = user.Gender,
      Locale = user.Locale,
      TimeZone = user.TimeZone,
      Picture = user.Picture,
      Profile = user.Profile,
      Website = user.Website,
      CustomAttributes = user.CustomAttributes
    };
    user = await _userService.UpdateAsync(user.Id, input, cancellationToken);

    return Ok(_mapper.Map<UserProfile>(user));
  }

  [Authorize]
  [HttpPut("profile/personal")]
  public async Task<ActionResult<UserProfile>> SavePersonalInformationAsync([FromBody] SavePersonalInformationPayload payload, CancellationToken cancellationToken)
  {
    User user = HttpContext.GetUser() ?? throw new InvalidOperationException("The User is required.");

    UpdateUserInput input = new()
    {
      Address = user.Address == null ? null : new AddressInput
      {
        Line1 = user.Address.Line1,
        Line2 = user.Address.Line2,
        Locality = user.Address.Locality,
        PostalCode = user.Address.PostalCode,
        Country = user.Address.Country,
        Region = user.Address.Region,
        Verify = false
      },
      Email = user.Email == null ? null : new EmailInput
      {
        Address = user.Email.Address,
        Verify = false
      },
      Phone = user.Phone == null ? null : new PhoneInput
      {
        CountryCode = user.Phone.CountryCode,
        Number = user.Phone.Number,
        Extension = user.Phone.Extension,
        Verify = false
      },
      FirstName = payload.FirstName,
      MiddleName = payload.MiddleName,
      LastName = payload.LastName,
      Nickname = payload.Nickname,
      Birthdate = payload.Birthdate,
      Gender = payload.Gender,
      Locale = payload.Locale,
      TimeZone = payload.TimeZone,
      Picture = payload.Picture,
      Profile = payload.Profile,
      Website = payload.Website,
      CustomAttributes = user.CustomAttributes
    };
    user = await _userService.UpdateAsync(user.Id, input, cancellationToken);

    return Ok(_mapper.Map<UserProfile>(user));
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
  public async Task<ActionResult<UserProfile>> SignInAsync([FromBody] SignInPayload payload, CancellationToken cancellationToken)
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

      return Ok(_mapper.Map<UserProfile>(session.User));
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
