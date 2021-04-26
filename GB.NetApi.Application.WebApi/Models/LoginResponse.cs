using System;
using System.Collections.Generic;

namespace GB.NetApi.Application.WebApi.Models
{
    /// <summary>
    /// Represents a response when a user has been successfully logged in
    /// </summary>
    [Serializable]
    public sealed record LoginResponse
    {
        public string Token { get; init; }

        public IEnumerable<string> Permissions { get; init; }
    }
}
