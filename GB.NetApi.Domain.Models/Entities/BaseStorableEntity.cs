namespace GB.NetApi.Domain.Models.Entities
{
    /// <summary>
    /// Represents an abstract storable entity which is forced to have an ID
    /// </summary>
    public abstract record BaseStorableEntity
    {
        public int ID { get; init; }
    }
}
