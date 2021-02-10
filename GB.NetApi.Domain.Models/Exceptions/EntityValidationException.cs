using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GB.NetApi.Domain.Models.Exceptions
{
    /// <summary>
    /// Represents an exception to throw when an entity validation process fails
    /// </summary>
    [Serializable]
    public sealed class EntityValidationException : Exception
    {
        #region Properties

        public readonly IEnumerable<string> Errors;

        #endregion

        public EntityValidationException() { }

        public EntityValidationException(string message) : base(message) { }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException) { }

        public EntityValidationException(IEnumerable<string> errors) => Errors = errors;

        private EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
