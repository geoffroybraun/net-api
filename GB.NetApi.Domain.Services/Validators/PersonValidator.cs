using GB.NetApi.Domain.Models.Entities;
using GB.NetApi.Domain.Services.Extensions;
using System;

namespace GB.NetApi.Domain.Services.Validators
{
    /// <summary>
    /// Validate a <see cref="Person"/> entity
    /// </summary>
    public sealed class PersonValidator
    {
        public event Action<string, object[]> SendErrorMessageEvent = delegate { };

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
            result &= IsSuperiorTo(person.ID, 0, nameof(Person.ID));
            result &= IsNotNullNorEmptyNorWhiteSpace(person.Firstname, nameof(Person.Firstname));
            result &= IsNotNullNorEmptyNorWhiteSpace(person.Lastname, nameof(Person.Lastname));
            result &= IsInferiorOrEqualTo(person.Birthdate, maxBirthdate, nameof(Person.Birthdate));

            return result;
        }

        #region Private methods

        private bool IsSuperiorTo(int value, int other, string propertyName)
        {
            if (value.IsInferiorOrEqualTo(other))
            {
                SendErrorMessageEvent("IntegerIsSuperiorTo", new object[] { propertyName, other });

                return false;
            }

            return true;
        }

        private bool IsNotNullNorEmptyNorWhiteSpace(string value, string propertyName)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                SendErrorMessageEvent("StringIsNullOrEmptyOrWhiteSpace", new[] { propertyName });

                return false;
            }

            return true;
        }

        private bool IsInferiorOrEqualTo(DateTime value, DateTime other, string propertyName)
        {
            if (value.IsSuperiorTo(other))
            {
                SendErrorMessageEvent("DateTimeIsSuperiorTo", new object[] { propertyName, other });

                return false;
            }

            return true;
        }

        #endregion
    }
}
