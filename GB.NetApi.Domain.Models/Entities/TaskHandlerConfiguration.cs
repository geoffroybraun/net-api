namespace GB.NetApi.Domain.Models.Entities
{
    /// <summary>
    /// Represents a configuration used by a <see cref="Interfaces.Libraries.ITaskHandler"/> implementation
    /// </summary>
    public sealed record TaskHandlerConfiguration
    {
        public int MaxRetriesCount { get; init; }

        public int MaxExceptionsCount { get; init; }

        public int TimeBetweenBreaksInMilliseconds { get; init; }

        public int TimeoutInMilliseconds { get; init; }

        public int MaxParalellizationCount { get; init; }

        public int MaxQueuedActionsCount { get; init; }
    }
}
