using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    public interface IRequestResponseHandler<in TRequest, TResponse>
    {
        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
