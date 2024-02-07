using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rebus.Bus;
using Rebus.Extensions;
using Rebus.Handlers;
using Rebus.Messages;
using Rebus.Retry.Simple;
using System;
using System.Threading.Tasks;

namespace EAU.Common
{
    public class FailedMessageOptions
    {
        public int QUEUES_MAX_ATTEMPTS { get; set; }
        public TimeSpan QUEUES_MAX_RETRY_INTERVAL { get; set; }
        public TimeSpan QUEUES_RETRY_INTERVAL_INCREMENT { get; set; }
    }

    public class FailedMessageHandler : IHandleMessages<IFailed<object>>
    {
        private readonly IBus _bus;
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<FailedMessageOptions> _optionsMonitor;

        public FailedMessageHandler(IBus bus, ILogger<FailedMessageHandler> logger, IOptionsMonitor<FailedMessageOptions> optionsMonitor)
        {
            _bus = bus;
            _logger = logger;
            _optionsMonitor = optionsMonitor;
        }

        public Task Handle(IFailed<object> message)
        {
            var msgID = message.Headers.GetValue(Headers.MessageId);

            var attempt = 1;

            var options = _optionsMonitor.CurrentValue;

            if (message.Headers.TryGetValue(Headers.DeferCount, out string deferCountString))
                attempt += int.Parse(deferCountString);

            if (attempt >= options.QUEUES_MAX_ATTEMPTS)
                throw new ApplicationException(string.Format("Message attempts count exceeded - {0}", options.QUEUES_MAX_ATTEMPTS), new AggregateException(message.Exceptions));

            var deferTime = new TimeSpan(options.QUEUES_RETRY_INTERVAL_INCREMENT.Ticks * attempt);

            deferTime = deferTime > options.QUEUES_MAX_RETRY_INTERVAL ? options.QUEUES_MAX_RETRY_INTERVAL : deferTime;

            _logger.LogInformation("Defer message {0} for {1}", msgID, deferTime);

            /*Try to process the message after specified period of time*/
            return _bus.Advanced.TransportMessage.Defer(deferTime);
        }
    }
}
