using GB.NetApi.Application.Services.Commands.Persons;
using GB.NetApi.Application.Services.DTOs;
using GB.NetApi.Application.Services.Queries.Persons;
using GB.NetApi.Application.WebApi.Authorizations;
using GB.NetApi.Application.WebApi.Extensions;
using GB.NetApi.Domain.Models.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GB.NetApi.Application.WebApi.Controllers
{
    /// <summary>
    /// Execute an action related to the <see cref="PersonDto"/>
    /// </summary>
    [Route("persons")]
    [ApiController]
    [Permission("ReadPerson")]
    public sealed class PersonController : BaseController
    {
        /// <summary>
        /// Instanciate a new <see cref="PersonController"/>
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/> implementation to use</param>
        /// <param name="translator">The <see cref="ITranslator"/> implementation to use</param>
        public PersonController(IMediator mediator, ITranslator translator) : base(mediator, translator) { }

        /// <summary>
        /// Create a <see cref="PersonDto"/>
        /// </summary>
        /// <param name="command">The command to run</param>
        /// <returns>True if the <see cref="PersonDto"/> has been successfully created, otherwise false</returns>
        [HttpPut]
        [Permission("WritePerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CreateAsync(CreatePersonCommand command)
        {
            if (command is null)
                return new BadRequestResult();

            var result = await Mediator.RunAsync(command).ConfigureAwait(false);

            return result ? NoContent() : InternalServerError();
        }

        /// <summary>
        /// Delete a <see cref="PersonDto"/> using its ID
        /// </summary>
        /// <param name="ID">The <see cref="PersonDto"/> ID to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{ID}")]
        [Permission("WritePerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAsync(int ID)
        {
            var result = await Mediator.RunAsync(new DeletePersonCommand() { ID = ID }).ConfigureAwait(false);

            return result ? NoContent() : InternalServerError();
        }

        /// <summary>
        /// Retrieve all filtered <see cref="PersonDto"/>
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <returns>All filtered <see cref="PersonDto"/></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> FilterAsync(FilterPersonQuery query)
        {
            var result = await Mediator.ExecuteAsync(query ?? new()).ConfigureAwait(false);

            return result is not null ? Ok(result) : NotFound(Translator.GetString("NoPersonFound"));
        }

        /// <summary>
        /// Retrieve a <see cref="PersonDto"/> using its ID
        /// </summary>
        /// w<param name="ID">The <see cref="PersonDto"/> ID to look for</param>
        /// <returns>The found <see cref="PersonDto"/></returns>
        [HttpGet]
        [Route("{ID}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAsync(int ID)
        {
            var result = await Mediator.ExecuteAsync(new GetSinglePersonQuery() { ID = ID }).ConfigureAwait(false);

            return result is not null ? Ok(result) : NotFound(Translator.GetString("NoPersonWithIDFound", new[] { ID }));
        }

        /// <summary>
        /// Retrieve all stored <see cref="PersonDto"/>
        /// </summary>
        /// <returns>All stored <see cref="PersonDto"/></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ListAsync()
        {
            var result = await Mediator.ExecuteAsync(new FilterPersonQuery()).ConfigureAwait(false);

            return result is not null ? Ok(result) : NotFound(Translator.GetString("NoPersonFound"));
        }

        /// <summary>
        /// Update an existing <see cref="PersonDto"/>
        /// </summary>
        /// <param name="ID">The <see cref="PersonDto"/> ID to update</param>
        /// <param name="command">The command to run</param>
        /// <returns>True if the <see cref="PersonDto"/> has been successfully updated, otherwise false</returns>
        [HttpPut]
        [Route("{ID}")]
        [Permission("WritePerson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAsync(int ID, [FromBody] CreatePersonCommand command)
        {
            if (command is null)
                return new BadRequestResult();

            var updateCommand = new UpdatePersonCommand()
            {
                Birthdate = command.Birthdate,
                Firstname = command.Firstname,
                ID = ID,
                Lastname = command.Lastname
            };
            var result = await Mediator.RunAsync(updateCommand).ConfigureAwait(false);

            return result ? NoContent() : InternalServerError();
        }
    }
}
