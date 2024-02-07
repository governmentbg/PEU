
namespace EAU.Users
{
    /// <summary>
    /// Интерфейс на услуга за работа с пароли.
    /// </summary>
    public interface IUserPasswordService
    {
        /// <summary>
        /// Валидиране чрез хеш.
        /// </summary>
        /// <param name="input">парола</param>
        /// <param name="passwordHash">хеш на парола</param>
        /// <returns></returns>
        bool ValidateWithHash(string input, string passwordHash);

        /// <summary>
        /// Генерира хеш на подадена в явен вид парола.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string CreateHash(string password);
    }
}
