namespace GB.NetApi.Application.WebApi.Models
{
    /// <summary>
    /// Represents the required configuration for the authentication process
    /// </summary>
    public sealed record JwtTokenConfiguration
    {
        public string Audience { get; init; }

        public string Issuer { get; init; }

        public string Key { get; init; }
    }
}
