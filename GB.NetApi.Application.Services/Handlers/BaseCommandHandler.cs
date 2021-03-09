using GB.NetApi.Application.Services.Collectors;
using GB.NetApi.Application.Services.Interfaces.Commands;
using GB.NetApi.Domain.Models.Interfaces.Services;
using GB.NetApi.Domain.Services.Extensions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GB.NetApi.Application.Services.Handlers
{
    /// <summary>
    /// Represents an abstract handler which run a <see cref="TCommand"/>
    /// </summary>
    /// <typeparam name="TCommand">The command type to run</typeparam>
    /// <typeparam name="TResult">The command result type to return</typeparam>
    public abstract class BaseCommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        #region Properties

        protected readonly ITranslator Translator;
        protected readonly MessagesCollector Collector;

        #endregion

        protected BaseCommandHandler() { }

        protected BaseCommandHandler(ITranslator translator)
        {
            Translator = translator ?? throw new ArgumentNullException(nameof(translator));
            Collector = new MessagesCollector();
        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await RunAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Delegate the method implementation to the deriving class
        /// </summary>
        /// <param name="command">The <see cref="TCommand"/> to run</param>
        /// <returns>The <see cref="TCommand"/> result</returns>
        public abstract Task<TResult> RunAsync(TCommand command);

        /// <summary>
        /// Translate the provided message using the optional parameters and collects the result
        /// </summary>
        /// <param name="message">The message to translate</param>
        /// <param name="parameters">The optional parameters to format the message with</param>
        protected void TranslateAndCollect(string message, params object[] parameters)
        {
            message = parameters.IsNotNullNorEmpty() ? Translator.GetString(message, parameters) : Translator.GetString(message);
            Collector.Collect(message);
        }
    }
}
