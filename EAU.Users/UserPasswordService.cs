namespace EAU.Users
{    
    /// <summary>
    /// Имплементация на IPasswordValidationService услуга за валидиране на парола по генериран преди това хеш.
    /// </summary>
    public class UserPasswordService : IUserPasswordService
    {
        public bool ValidateWithHash(string input, string passwordHash) => BCrypt.Net.BCrypt.Verify(input, passwordHash);

        public string CreateHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}
