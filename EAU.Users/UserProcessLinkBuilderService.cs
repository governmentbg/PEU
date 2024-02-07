using EAU.Users.Models;
using System;

namespace EAU.Users
{
    /// <summary>
    /// Предоставя методи за генериране на линкове по процеси свързани с потребители.
    /// </summary>
    public interface IUserProcessLinkBuilderService
    {
        /// <summary>
        /// Генериране на линк за потвърждаване регистрация.
        /// </summary>
        Uri BuildConfirmRegistrationLink(Uri baseUri, UserProcess userProcess);

        /// <summary>
        /// Генериране на линк за потвърждаване регистрация.
        /// </summary>
        Uri BuildCancelRegistrationLink(Uri baseUri, UserProcess userProcess);

        /// <summary>
        /// Генериране на линк за промяна на парола
        /// </summary>
        Uri BuildChangePasswordLink(Uri baseUri, UserProcess passProcess);
    }

    internal class UserProcessLinkBuilderService : IUserProcessLinkBuilderService
    {
        public Uri BuildConfirmRegistrationLink(Uri baseUri, UserProcess userProcess)
        {
            if (userProcess.ProcessType != UserProcessTypes.Registration)
                throw new InvalidOperationException("userProcess.ProcessType is not Registration");

            string relativeUri = $"users/confirmRegistration/{userProcess.ProcessGuid.ToString().ToLower()}";

            return new Uri(baseUri: baseUri, relativeUri: relativeUri);
        }

        public Uri BuildCancelRegistrationLink(Uri baseUri, UserProcess userProcess)
        {
            if (userProcess.ProcessType != UserProcessTypes.Registration)
                throw new InvalidOperationException("userProcess.ProcessType is not Registration");

            string relativeUri = $"users/cancelRegistration/{userProcess.ProcessGuid.ToString().ToLower()}";

            return new Uri(baseUri: baseUri, relativeUri: relativeUri);
        }

        public Uri BuildChangePasswordLink(Uri baseUri, UserProcess passProcess)
        {
            if (passProcess.ProcessType != UserProcessTypes.ChangePassword)
                throw new InvalidOperationException("passProcess.ProcessType is not ChangePassword");

            string relativeUri = $"users/forgottenPassword/{passProcess.ProcessGuid.ToString().ToLower()}";

            return new Uri(baseUri: baseUri, relativeUri: relativeUri);
        }
    }
}
