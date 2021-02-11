using GB.NetApi.Application.Services.Commands.Persons;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Controllers
{
    [Route("persons")]
    [ApiController]
    public sealed class PersonController : ControllerBase
    {
        #region Fields

        private readonly IMediator Mediator;

        #endregion

        public PersonController(IMediator mediator) => Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPut]
        public async Task<bool> CreateAsync(CreatePersonCommand command) => await Mediator.Send(command).ConfigureAwait(false);
    }
}
