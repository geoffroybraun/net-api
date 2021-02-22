namespace GB.NetApi.Domain.Models.Entities.Identity
{
    /// <summary>
    /// Represents a permission which connects an operation and a resource
    /// </summary>
    public sealed record Permission : BaseStorableEntity
    {
        public string Name { get; init; }

        public int OperationID { get; init; }

        public int ResourceID { get; init; }
    }
}
