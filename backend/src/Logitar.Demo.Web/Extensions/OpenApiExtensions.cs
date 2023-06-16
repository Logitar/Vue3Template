using Microsoft.OpenApi.Models;
using DemoApplication = Logitar.Demo.Web.Constants.Application;

namespace Logitar.Demo.Web.Extensions;

internal static class OpenApiExtensions
{
  private const string Title = "Demo API";

  internal static IServiceCollection AddOpenApi(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(config =>
    {
      //config.AddSecurity();
      config.SwaggerDoc(name: $"v{DemoApplication.Version.Major}", new OpenApiInfo
      {
        Contact = new OpenApiContact
        {
          Email = "francispion@hotmail.com",
          Name = "Logitar Team",
          Url = new Uri("https://github.com/Logitar/Vue3Template", UriKind.Absolute)
        },
        Description = "Demo application.",
        License = new OpenApiLicense
        {
          Name = "Use under MIT",
          Url = new Uri("https://github.com/Logitar/Vue3Template/blob/main/LICENSE", UriKind.Absolute)
        },
        Title = Title,
        Version = $"v{DemoApplication.Version}"
      });
    });

    return services;
  }

  internal static void UseOpenApi(this IApplicationBuilder builder)
  {
    builder.UseSwagger();
    builder.UseSwaggerUI(config => config.SwaggerEndpoint(
      url: $"/swagger/v{DemoApplication.Version.Major}/swagger.json",
      name: $"{Title} v{DemoApplication.Version}"
    ));
  }

  //private static void AddSecurity(this SwaggerGenOptions options)
  //{
  //  options.AddSecurityDefinition(Schemes.Basic, new OpenApiSecurityScheme
  //  {
  //    Description = "Enter your credentials in the inputs below:",
  //    In = ParameterLocation.Header,
  //    Name = Headers.Authorization,
  //    Scheme = Schemes.Basic,
  //    Type = SecuritySchemeType.Http
  //  });
  //  options.AddSecurityRequirement(new OpenApiSecurityRequirement
  //  {
  //    {
  //      new OpenApiSecurityScheme
  //      {
  //        In = ParameterLocation.Header,
  //        Name = Headers.Authorization,
  //        Reference = new OpenApiReference
  //        {
  //          Id = Schemes.Basic,
  //          Type = ReferenceType.SecurityScheme
  //        },
  //        Scheme = Schemes.Basic,
  //        Type = SecuritySchemeType.Http
  //      },
  //      new List<string>()
  //    }
  //  });
  //}
}
