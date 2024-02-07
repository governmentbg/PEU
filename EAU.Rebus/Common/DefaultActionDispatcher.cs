using Rebus.Bus;
using System.Threading.Tasks;

namespace EAU.Common
{
    public class DefaultActionDispatcher : IActionDispatcher
    {
        private readonly IBus Bus;

        public DefaultActionDispatcher(IBus bus)
        {
            Bus = bus;
        }

        public Task SendAsync(object actionData) => Bus.Send(actionData);
    }
}
