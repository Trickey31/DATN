using Workspace.Application;
using static Workspace.Contract.Response;

namespace Workspace.Contract
{
    public static class Queries
    {
        public record GetTaskQuery(string? SearchTerm, string? SortColumn, 
            SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder,
            int PageIndex, int PageSize) : IQuery<PagedResult<TaskResponse>>;

        public record GetTaskIdQuery(Guid id) : IQuery<TaskResponse>;
    }
}
