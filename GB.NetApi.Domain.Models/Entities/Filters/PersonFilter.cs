namespace GB.NetApi.Domain.Models.Entities.Filters
{
    /// <summary>
    /// Represents a filter which retrieves matching <see cref="Person"/> entities
    /// </summary>
    public sealed record PersonFilter
    {
        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public int BirthYear { get; init; }

        public int BirthMonth { get; init; }

        public int BirthDay { get; init; }
    }
}
