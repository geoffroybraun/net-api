namespace GB.NetApi.Application.WebApi.Models
{
    /// <summary>
    /// Represents the required configuration for the CORS policy
    /// </summary>
    public sealed record CorsConfiguration
    {
        public string Origin { get; init; }

        public string[] Headers { get; init; }

        public bool AllowCredentials { get; init; }
    }
}
