using GB.NetApi.Domain.Models.Entities;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GB.NetApi.Domain.Services.UnitTests", AllInternalsVisible = true)]
[assembly: InternalsVisibleTo("GB.NetApi.Infrastructure.Database", AllInternalsVisible = true)]
[assembly: InternalsVisibleTo("GB.NetApi.Infrastructure.Libraries", AllInternalsVisible = true)]
[assembly: InternalsVisibleTo("GB.NetApi.Application.Services", AllInternalsVisible = true)]
[assembly: InternalsVisibleTo("GB.NetApi.Application.Services.UnitTests", AllInternalsVisible = true)]
namespace GB.NetApi.Domain.Services.Validators
{
    /// <summary>
    /// Validate a <see cref="Person"/> entity
    /// </summary>
    internal sealed class PersonValidator : BaseValidator
    {
        /// <summary>
        /// Indicates if the provided <see cref="Person"/> entity is not valid
        /// </summary>
        /// <param name="person">The <see cref="Person"/> entity to validate</param>
        /// <returns>True if the provided <see cref="Person"/> is not valid, otherwise false</returns>
        public bool IsNotValid(Person person, DateTime maxBirthdate) => !IsValid(person, maxBirthdate);

        /// <summary>
        /// Indicates if the provided <see cref="Person"/> entity is valid
        /// </summary>
        /// <param name="person">The <see cref="Person"/> entity to validate</param>
        /// <param name="maxBirthdate">The <see cref="DateTime"/> to be inferior or equal to</param>
        /// <returns>True if the provided <see cref="Person"/> is valid, otherwise false</returns>
        public bool IsValid(Person person, DateTime maxBirthdate)
        {
            if (person is null)
                return false;

            var result = true;
            result &= IsNotNullNorEmptyNorWhiteSpace(person.Firstname, nameof(Person.Firstname));
            result &= IsNotNullNorEmptyNorWhiteSpace(person.Lastname, nameof(Person.Lastname));
            result &= IsInferiorOrEqualTo(person.Birthdate, maxBirthdate, nameof(Person.Birthdate));

            return result;
        }

        /// <summary>
        /// Indicates if the provided <see cref="Person"/> entity is valid, including its <see cref="Person.ID"/> property, or not
        /// </summary>
        /// <param name="person">The <see cref="Person"/> entity to validate</param>
        /// <param name="maxBirthdate">The <see cref="DateTime"/> to be inferior or equal to</param>
        /// <returns>True if the provided <see cref="Person"/> entity is not valid, including its <see cref="Person.ID"/> property, otherwise false</returns>
        public bool IsNotValidWithID(Person person, DateTime maxBirthdate) => !IsValidWithID(person, maxBirthdate);

        /// <summary>
        /// Indicates if the provided <see cref="Person"/> entity is valid, including its <see cref="Person.ID"/> property
        /// </summary>
        /// <param name="person">The <see cref="Person"/> entity to validate</param>
        /// <param name="maxBirthdate">The <see cref="DateTime"/> to be inferior or equal to</param>
        /// <returns>True if the provided <see cref="Person"/> entity is valid, including its <see cref="Person.ID"/> property, otherwise false</returns>
        public bool IsValidWithID(Person person, DateTime maxBirthdate)
        {
            if (person is null)
                return false;

            var result = true;
            result &= IsSuperiorTo(person.ID, 0, nameof(Person.ID));
            result &= IsValid(person, maxBirthdate);

            return result;
        }
    }
}
