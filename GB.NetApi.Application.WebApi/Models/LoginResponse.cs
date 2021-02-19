using System;

namespace GB.NetApi.Application.WebApi.Models
{
    /// <summary>
    /// Represents a response sent when loging in a user
    /// </summary>
    public sealed record LoginResponse
    {
        public string Token { get; init; }

        public DateTime ExpirationDateTime { get; init; }
    }
}
