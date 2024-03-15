using MediatR;
using Workspace.Contract;

namespace Workspace.Application
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResult<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
