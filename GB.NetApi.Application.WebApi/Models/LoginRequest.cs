namespace GB.NetApi.Application.WebApi.Models
{
    /// <summary>
    /// Represents a request to log a user in
    /// </summary>
    public sealed record LoginRequest
    {
        public string UserName { get; init; }

        public string Password { get; init; }
    }
}
