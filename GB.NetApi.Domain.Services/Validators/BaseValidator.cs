using GB.NetApi.Domain.Services.Extensions;
using System;

namespace GB.NetApi.Domain.Services.Validators
{
    /// <summary>
    /// Represents an abstract entity validator which provides useful methods to deriving classes
    /// </summary>
    internal abstract class BaseValidator
    {
        #region Events

        public event Action<string, object[]> SendErrorMessageEvent = delegate { };

        #endregion

        /// <summary>
        /// Indicates if the provided <see cref="int"/> value is superior to the other one
        /// </summary>
        /// <param name="value">The <see cref="int"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="int"/> value to compare to the provided one</param>
        /// <param name="propertyName">The property name to send through the <see cref="SendErrorMessageEvent"/> event</param>
        /// <returns>True if the <see cref="int"/> value is superior to the other one, otherwise false</returns>
        protected bool IsSuperiorTo(int value, int other, string propertyName)
        {
            if (value.IsInferiorOrEqualTo(other))
            {
                SendErrorMessageEvent("IntegerMustBeSuperiorTo", new object[] { propertyName, other });

                return false;
            }

            return true;
        }

        /// <summary>
        /// Indicates if the provided <see cref="string"/> value is not null, nor empty, nor white space
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to validate</param>
        /// <param name="propertyName">The property name to send through the <see cref="SendErrorMessageEvent"/> event</param>
        /// <returns>True if the provided <see cref="string"/> value is not null, nor empty, nor white space, otherwise false</returns>
        protected bool IsNotNullNorEmptyNorWhiteSpace(string value, string propertyName)
        {
            if (value.IsNullOrEmptyOrWhiteSpace())
            {
                SendErrorMessageEvent("StringIsNullOrEmptyOrWhiteSpace", new[] { propertyName });

                return false;
            }

            return true;
        }

        /// <summary>
        /// Indicates if the provided <see cref="DateTime"/> value is inferior or equal to the other one
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to compare to the other one</param>
        /// <param name="other">The other <see cref="DateTime"/> value to compare to the provided one</param>
        /// <param name="propertyName">The property name to send through the <see cref="SendErrorMessageEvent"/> event</param>
        /// <returns>True if the provided <see cref="DateTime"/> value is inferior or equal to the other one, otherwise false</returns>
        protected bool IsInferiorOrEqualTo(DateTime value, DateTime other, string propertyName)
        {
            if (value.IsSuperiorTo(other))
            {
                SendErrorMessageEvent("DateTimeMustBeInferiorOrEqualTo", new object[] { propertyName, other });

                return false;
            }

            return true;
        }
    }
}
