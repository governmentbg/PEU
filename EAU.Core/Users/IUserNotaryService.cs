using CNSys;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    /// <summary>
    /// Резултат от проверка на текущ потребител за права на нотариус
    /// </summary>
    public class UserNotaryInfo
    {
        public UserNotaryInfo() { }

        public UserNotaryInfo(IErrorCollection errors)
        {
            Errors = errors;
        }

        public bool HasNotaryRights { get; set; }
        public string NotaryNumber { get; set; }
        public IErrorCollection Errors { get; private set; }
    }

    /// <summary>
    /// Интерфейс за проверка на текущ потребител за права на нотариус
    /// </summary>
    public interface IUserNotaryService
    {
        Task<UserNotaryInfo> GetUserNotaryInfoAsync(CancellationToken cancellationToken);
    }
}
