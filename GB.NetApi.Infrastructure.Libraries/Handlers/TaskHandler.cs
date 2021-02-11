using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Models.Interfaces.Libraries;
using Polly;
using Polly.Wrap;
using System;
using System.Threading.Tasks;

namespace GB.NetApi.Infrastructure.Libraries.Handlers
{
    /// <summary>
    /// Handles a task function by applying bulkhead, timeout, breaker and retries policies
    /// </summary>
    public sealed class TaskHandler : ITaskHandler
    {
        #region Fields

        private static readonly TaskHandlerConfiguration DefaultConfiguration = new TaskHandlerConfiguration()
        {
            MaxExceptionsCount = 3,
            MaxParalellizationCount = 2,
            MaxQueuedActionsCount = 20,
            MaxRetriesCount = 5,
            TimeBetweenBreaksInMilliseconds = 500,
            TimeoutInMilliseconds = 25000
        };
        private readonly TaskHandlerConfiguration Configuration;

        #endregion

        public TaskHandler() : this(DefaultConfiguration) { }

        public TaskHandler(TaskHandlerConfiguration configuration) => Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        public async Task<TResult> HandleAsync<TResult>(Func<Task<TResult>> taskFunction)
        {
            var wrapper = GetPoliciesWrapper<TResult>(Configuration);

            return await wrapper.ExecuteAsync(taskFunction)
                .ConfigureAwait(false);
        }

        #region Private methods

        private static AsyncPolicyWrap<TResult> GetPoliciesWrapper<TResult>(TaskHandlerConfiguration configuration)
        {
            var retries = GetRetriesPolicy<TResult>(configuration);
            var breaker = GetCircuitBreakPolicy<TResult>(configuration);
            var timeout = GetTimeoutPolicy<TResult>(configuration);
            var bulkhead = GetBulkheadPolicy<TResult>(configuration);

            return Policy.WrapAsync(retries, breaker, timeout, bulkhead);
        }

        private static IAsyncPolicy<TResult> GetRetriesPolicy<TResult>(TaskHandlerConfiguration configuration)
        {
            var maxRetriesCount = configuration.MaxRetriesCount;
            static TimeSpan retryAttemptFunction(int retryAttempt) => TimeSpan.FromMilliseconds(Math.Pow(2, retryAttempt));

            return Policy<TResult>.Handle<Exception>()
                .WaitAndRetryAsync(maxRetriesCount, retryAttemptFunction);
        }

        private static IAsyncPolicy<TResult> GetCircuitBreakPolicy<TResult>(TaskHandlerConfiguration configuration)
        {
            var maxExceptionsCount = configuration.MaxExceptionsCount;
            var timeBetweenExceptionsTimeSpan = TimeSpan.FromMilliseconds(configuration.TimeBetweenBreaksInMilliseconds);

            return Policy<TResult>.Handle<Exception>()
                .CircuitBreakerAsync(maxExceptionsCount, timeBetweenExceptionsTimeSpan);
        }

        private static IAsyncPolicy<TResult> GetTimeoutPolicy<TResult>(TaskHandlerConfiguration configuration)
        {
            var timeoutTimeSpan = TimeSpan.FromMilliseconds(configuration.TimeoutInMilliseconds);

            return Policy.TimeoutAsync<TResult>(timeoutTimeSpan);
        }

        private static IAsyncPolicy<TResult> GetBulkheadPolicy<TResult>(TaskHandlerConfiguration configuration)
        {
            var maxParallelizationCount = configuration.MaxParalellizationCount;
            var maxQueuedActionsCount = configuration.MaxQueuedActionsCount;

            return Policy.BulkheadAsync<TResult>(maxParallelizationCount, maxQueuedActionsCount);
        }

        #endregion
    }
}
