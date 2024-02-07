using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Net.Http
{
    public class TimeoutHandler : DelegatingHandler
    {
        private CancellationTokenSource _pendingRequestsCts;
        public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(99);

        public TimeoutHandler()
        {
            _pendingRequestsCts = new CancellationTokenSource();
        }

        public TimeoutHandler(double seconds) : this()
        {
            DefaultTimeout = TimeSpan.FromSeconds(seconds);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // We need a CancellationTokenSource to use with the request.  We always have the global
            // _pendingRequestsCts to use, plus we may have a token provided by the caller, and we may
            // have a timeout.  If we have a timeout or a caller-provided token, we need to create a new
            // CTS (we can't, for example, timeout the pending requests CTS, as that could cancel other
            // unrelated operations).  Otherwise, we can use the pending requests CTS directly.
            CancellationTokenSource cts;
            bool disposeCts;
            var _timeout = DefaultTimeout;
            bool hasTimeout = _timeout != Timeout.InfiniteTimeSpan;
            long timeoutTime = long.MaxValue;
            if (hasTimeout || cancellationToken.CanBeCanceled)
            {
                disposeCts = true;
                cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _pendingRequestsCts.Token);
                if (hasTimeout)
                {
                    timeoutTime = Environment.TickCount + (_timeout.Ticks / TimeSpan.TicksPerMillisecond);
                    cts.CancelAfter(_timeout);
                }
            }
            else
            {
                disposeCts = false;
                cts = _pendingRequestsCts;
            }

            try
            {
                return await base.SendAsync(request, cts.Token);
            }
            catch (Exception e)
            {
                HandleFinishSendAsyncCleanup(cts, disposeCts);

                if (e is OperationCanceledException operationException && TimeoutFired(cancellationToken, timeoutTime))
                {
                    throw CreateTimeoutException(operationException, _timeout);
                }

                throw;
            }
        }

        private TimeoutException CreateTimeoutException(OperationCanceledException originalException, TimeSpan _timeout)
        {
            return new TimeoutException(String.Format("Timeout after {0} seconds", _timeout.TotalSeconds));
        }

        private bool TimeoutFired(CancellationToken callerToken, long timeoutTime)
            => !callerToken.IsCancellationRequested && Environment.TickCount >= timeoutTime;

        private void HandleFinishSendAsyncCleanup(CancellationTokenSource cts, bool disposeCts)
        {
            // Dispose of the CancellationTokenSource if it was created specially for this request
            // rather than being used across multiple requests.
            if (disposeCts)
            {
                cts.Dispose();
            }

            // This method used to also dispose of the request content, e.g.:
            //     request.Content?.Dispose();
            // This has multiple problems:
            // 1. It prevents code from reusing request content objects for subsequent requests,
            //    as disposing of the object likely invalidates it for further use.
            // 2. It prevents the possibility of partial or full duplex communication, even if supported
            //    by the handler, as the request content may still be in use even if the response
            //    (or response headers) has been received.
            // By changing this to not dispose of the request content, disposal may end up being
            // left for the finalizer to handle, or the developer can explicitly dispose of the
            // content when they're done with it.  But it allows request content to be reused,
            // and more importantly it enables handlers that allow receiving of the response before
            // fully sending the request.  Prior to this change, a handler that supported duplex communication
            // would fail trying to access certain sites, if the site sent its response before it had
            // completely received the request: CurlHandler might then find that the request content
            // was disposed of while it still needed to read from it.
        }
    }
}
