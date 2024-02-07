using System.Threading.Tasks;

namespace EAU.Common
{
    public interface IActionDispatcher
    {
        Task SendAsync(object actionData);
    }
}
