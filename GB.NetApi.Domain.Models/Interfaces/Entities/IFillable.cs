namespace GB.NetApi.Domain.Models.Interfaces.Entities
{
    /// <summary>
    /// Represents an entity which can be filled from a <see cref="T"/> type
    /// </summary>
    /// <typeparam name="T">The type to be filled from</typeparam>
    public interface IFillable<in T>
    {
        /// <summary>
        /// Indicates if the implementing entity can be filled from the provided <see cref="T"/> type
        /// </summary>
        /// <param name="entity">The <see cref="T"/> type to be filled from</param>
        /// <returns>True if the implementing entity can be filled from the provided <see cref="T"/> type, otherwise false</returns>
        bool CanBeFilled(T entity);

        /// <summary>
        /// Fill the implementing entity from the provided <see cref="T"/> type
        /// </summary>
        /// <param name="entity">The <see cref="T"/> type to be filled from</param>
        void Fill(T entity);
    }
}
