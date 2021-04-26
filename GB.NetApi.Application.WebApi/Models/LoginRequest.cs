using System;

namespace GB.NetApi.Application.WebApi.Models
{
    /// <summary>
    /// Represents a request to log a user in
    /// </summary>
    [Serializable]
    public sealed record LoginRequest
    {
        public string Email { get; init; }

        public string Password { get; init; }
    }
}
