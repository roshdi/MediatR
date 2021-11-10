using System.Threading;
using System.Threading.Tasks;

namespace MediatR
{
    /// <summary>
    /// Send a request through the mediator pipeline to be handled by a single handler.
    /// </summary>
    public partial interface ISender
    {
        Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : notnull;
    }
}