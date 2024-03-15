using MediatR;
using Workspace.Contract;

namespace Workspace.Application
{
    public interface IQuery<TResponse> : IRequest<TResult<TResponse>>
    {
    }
}
