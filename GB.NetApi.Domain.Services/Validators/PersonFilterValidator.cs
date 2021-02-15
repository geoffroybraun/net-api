using GB.NetApi.Domain.Models.Entities.Filters;
using GB.NetApi.Domain.Services.Extensions;

namespace GB.NetApi.Domain.Services.Validators
{
    /// <summary>
    /// Validate a <see cref="PersonFilter"/> entity
    /// </summary>
    public static class PersonFilterValidator
    {
        /// <summary>
        /// Indicates if the provided <see cref="PersonFilter"/> entity is not valid
        /// </summary>
        /// <param name="filter">The <see cref="PersonFilter"/> entity to validate</param>
        /// <returns>True if the provided <see cref="PersonFilter"/> entity is not valid, otherwise false</returns>
        public static bool IsNotValid(PersonFilter filter) => !IsValid(filter);

        /// <summary>
        /// Indicates if the provided <see cref="PersonFilter"/> entity is valid
        /// </summary>
        /// <param name="filter">The <see cref="PersonFilter"/> entity to validate</param>
        /// <returns>True if the provided <see cref="PersonFilter"/> entity is valid, otherwise false</returns>
        public static bool IsValid(PersonFilter filter)
        {
            if (filter is null)
                return false;

            var result = false;
            result |= filter.Firstname.IsNotNullNorEmptyNorWhiteSpace();
            result |= filter.Lastname.IsNotNullNorEmptyNorWhiteSpace();
            result |= filter.BirthYear.IsSuperiorTo(0);
            result |= filter.BirthMonth.IsSuperiorTo(0) && filter.BirthMonth.IsInferiorOrEqualTo(12);
            result |= filter.BirthDay.IsSuperiorTo(0) && filter.BirthDay.IsInferiorOrEqualTo(31);

            return result;
        }
    }
}
