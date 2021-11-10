using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Wrappers
{
    internal class RequestResponseHandlerWrapper<TRequest, TResponse> : RequestHandlerBase
        where TRequest : notnull
    {
        public override Task<object?> Handle(object request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, ServiceFactory serviceFactory)
        {
            Task<TResponse> Handler() => GetHandler<IRequestResponseHandler<TRequest, TResponse>>(serviceFactory)
                .Handle((TRequest) request, cancellationToken);

            return serviceFactory
                .GetInstances<IPipelineBehavior<TRequest, TResponse>>()
                .Reverse()
                .Aggregate((RequestHandlerDelegate<TResponse>) Handler, (next, pipeline) => () => pipeline.Handle((TRequest) request, cancellationToken, next))();
        }
    }
}
