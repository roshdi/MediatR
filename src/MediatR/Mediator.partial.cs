namespace MediatR
{
    using Wrappers;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Default mediator implementation relying on single- and multi instance delegates for resolving handlers.
    /// </summary>
    public partial class Mediator : IMediator
    {
        public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : notnull
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var handler = (RequestResponseHandlerWrapper<TRequest, TResponse>) _requestHandlers.GetOrAdd(typeof(TRequest),
                t => (RequestHandlerBase) Activator.CreateInstance(typeof(RequestResponseHandlerWrapper<,>).MakeGenericType(typeof(TRequest), typeof(TResponse))));

            return handler.Handle(request, cancellationToken, _serviceFactory);



            // call via dynamic dispatch to avoid calling through reflection for performance reasons
            //return handler.Handle(request, cancellationToken, _serviceFactory);
            throw new Exception();
        }
    }
}
