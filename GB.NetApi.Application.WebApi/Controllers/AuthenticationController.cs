using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using GB.NetApi.Application.WebApi.Models;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Executes an action related to the authentication process
    /// </summary>
    [Route("")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        #region Fields

        private readonly UserManager<UserDao> Manager;
        private readonly JwtTokenConfiguration Configuration;

        #endregion

        public AuthenticationController(IMediator mediator, UserManager<UserDao> manager, JwtTokenConfiguration configuration) : base(mediator)
        {
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Log the <see cref="LoginRequest"/> user in by generating a JWT token
        /// </summary>
        /// <param name="request">The <see cref="LoginRequest"/> to use to generate the token</param>
        /// <returns>The generated token</returns>
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (!await IsValidAsync(request))
                throw new EntityValidationException(new[] { $"Request invalid" });

            var user = await ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserEmail = request.Email }).ConfigureAwait(false);
            var token = GenerateToken(GetClaimsFromUser(user));
            var tokenHandler = new JwtSecurityTokenHandler();

            return Ok(new LoginResponse() { Token = tokenHandler.WriteToken(token), ExpirationDateTime = token.ValidTo });
        }

        #region Private methods

        private async Task<bool> IsValidAsync(LoginRequest request)
        {
            var user = await Manager.FindByEmailAsync(request.Email).ConfigureAwait(false);

            return user is not null && await Manager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false);
        }

        private IEnumerable<Claim> GetClaimsFromUser(AuthenticateUserDto user)
        {
            var claims = new List<Claim>(user.Claims);
            claims.AddRange(user.PermissionNames.Select(e => new Claim("Permission", e)));

            return claims;
        }

        private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            return new JwtSecurityToken(
                audience: Configuration.Audience,
                issuer: Configuration.Issuer,
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: signingCredentials);
        }

        #endregion
    }
}
