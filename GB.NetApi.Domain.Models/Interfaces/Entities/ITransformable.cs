namespace GB.NetApi.Domain.Models.Interfaces.Entities
{
    /// <summary>
    /// Represents an entity which can be transformed to a <see cref="T"/> type
    /// </summary>
    /// <typeparam name="T">The type to be transformed to</typeparam>
    public interface ITransformable<T>
    {
        /// <summary>
        /// Returns the implementing entity to a <see cref="T"/> type
        /// </summary>
        /// <returns>A <see cref="T"/> type</returns>
        T Transform();
    }
}
