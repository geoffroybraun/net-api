using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Persons;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Execute an action related to the <see cref="PersonDto"/>
    /// </summary>
    [Route("persons")]
    [ApiController]
    public sealed class PersonController : BaseController
    {
        public PersonController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Create a <see cref="PersonDto"/>
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns>True if the <see cref="PersonDto"/> has been successfully created, otherwise false</returns>
        [HttpPut]
        public async Task<bool> CreateAsync(CreatePersonCommand command) => await RunAsync(command).ConfigureAwait(false);

        /// <summary>
        /// Retrieve all filtered <see cref="PersonDto"/>
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <returns>All filtered <see cref="PersonDto"/></returns>
        [HttpPost]
        public async Task<IEnumerable<PersonDto>> FilterAsync(FilterPersonQuery query) => await ExecuteAsync(query).ConfigureAwait(false);

        /// <summary>
        /// Retrieve a <see cref="PersonDto"/> using its ID
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <returns>The found <see cref="PersonDto"/></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var result = await ExecuteAsync(new GetSinglePersonQuery() { ID = id }).ConfigureAwait(false);

            return result is not null ? Ok(result) : NotFound($"No person with ID {id} found.");
        }

        /// <summary>
        /// Retrieve all stored <see cref="PersonDto"/>
        /// </summary>
        /// <returns>All stored <see cref="PersonDto"/></returns>
        [HttpGet]
        public async Task<IEnumerable<PersonDto>> ListAsync() => await ExecuteAsync(new FilterPersonQuery()).ConfigureAwait(false);
    }
}
