namespace GB.NetApi.Domain.Services.Validators
{
    /// <summary>
    /// Validate a <see cref="Models.Entities.Identity.AuthenticateUser"/> entity values
    /// </summary>
    internal sealed class AuthenticateUserValidator : BaseValidator
    {
        /// <summary>
        /// Indicates if the provided values are not valid
        /// </summary>
        /// <param name="userName">The user name to check</param>
        /// <param name="userEmail">The user email address to check</param>
        /// <returns>True if both values are not valid, otherwise false</returns>
        public bool IsNotValid(string userName, string userEmail) => !IsValid(userName, userEmail);

        /// <summary>
        /// Indicates if the provided values are valid
        /// </summary>
        /// <param name="userName">The user name to check</param>
        /// <param name="userEmail">The user email address to check</param>
        /// <returns>True if both values are valid, otherwise false</returns>
        public bool IsValid(string userName, string userEmail)
        {
            var result = true;
            result &= IsNotNullNorEmptyNorWhiteSpace(userName, "User name");
            result &= IsNotNullNorEmptyNorWhiteSpace(userEmail, "User email");

            return result;
        }
    }
}
