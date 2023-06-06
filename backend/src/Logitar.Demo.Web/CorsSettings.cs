namespace Logitar.Demo.Web;

internal record CorsSettings
{
  public bool AllowCredentials { get; set; }
  public string[]? AllowedHeaders { get; set; }
  public string[]? AllowedMethods { get; set; }
  public string[]? AllowedOrigins { get; set; }
}
