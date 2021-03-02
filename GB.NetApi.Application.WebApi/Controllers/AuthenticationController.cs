using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.AuthenticateUsers;
using GB.NetApi.Application.WebApi.Models;
using GB.NetApi.Domain.Models.Exceptions;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Infrastructure.Database.DAOs.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Executes an action related to the authentication process
    /// </summary>
    [Route("")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : BaseController
    {
        #region Fields

        private readonly UserManager<UserDao> Manager;
        private readonly JwtTokenConfiguration Configuration;

        #endregion

        /// <summary>
        /// Instanciates a new <see cref="AuthenticationController"/>
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/> implementation to use</param>
        /// <param name="translator">The <see cref="ITranslator"/> implementation to use</param>
        /// <param name="manager">THe <see cref="UserManager{UserDao}"/> to use when authenticating</param>
        /// <param name="configuration">The <see cref="JwtTokenConfiguration"/> to use when generating a token</param>
        public AuthenticationController(IMediator mediator, ITranslator translator, UserManager<UserDao> manager, JwtTokenConfiguration configuration) : base(mediator, translator)
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (!await IsValidAsync(request))
                throw new EntityValidationException(new[] { Translator.GetString("InvalidRequest") });

            var user = await ExecuteAsync(new GetSingleAuthenticateUserQuery() { UserEmail = request.Email }).ConfigureAwait(false);
            var token = GenerateToken(GetClaimsFromUser(user));

            return Ok(token);
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

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(Configuration.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expirationDateTime = DateTime.UtcNow.AddHours(Configuration.ValidityLifeTimeInHours);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = Configuration.Audience,
                Expires = expirationDateTime,
                Issuer = Configuration.Issuer,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        #endregion
    }
}
