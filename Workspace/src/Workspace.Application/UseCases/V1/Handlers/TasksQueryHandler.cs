using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Workspace.Contract;
using Workspace.Domain;
using Workspace.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Workspace.Application
{
    public sealed class TasksQueryHandler : IQueryHandler<Queries.GetTaskQuery, PagedResult<Response.TaskResponse>>
    {
        private readonly IRepositoryBase<Tasks, Guid> _taskRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TasksQueryHandler(IRepositoryBase<Tasks, Guid> taskRepository, IMapper mapper, ApplicationDbContext context)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<TResult<PagedResult<Response.TaskResponse>>> Handle(Queries.GetTaskQuery request, CancellationToken cancellationToken)
        {
            if (request.SortColumnAndOrder.Any()) // =>>  Raw Query when order by multi column
            {
                var PageIndex = request.PageIndex <= 0 ? PagedResult<Tasks>.DefaultPageIndex : request.PageIndex;
                var PageSize = request.PageSize <= 0
                    ? PagedResult<Tasks>.DefaultPageSize
                    : request.PageSize > PagedResult<Tasks>.UpperPageSize
                    ? PagedResult<Tasks>.UpperPageSize : request.PageSize;

                // ============================================
                var tasksQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                    ? @$"SELECT * FROM {nameof(Tasks)} ORDER BY "
                    : @$"SELECT * FROM {nameof(Tasks)}
                        WHERE {nameof(Tasks.Name)} LIKE '%{request.SearchTerm}%'
                        OR {nameof(Tasks.Description)} LIKE '%{request.SearchTerm}%'
                        ORDER BY ";

                foreach (var item in request.SortColumnAndOrder)
                    tasksQuery += item.Value == SortOrder.Descending
                        ? $"{item.Key} DESC, "
                        : $"{item.Key} ASC, ";

                tasksQuery = tasksQuery.Remove(tasksQuery.Length - 2);

                tasksQuery += $" OFFSET {(PageIndex - 1) * PageSize} ROWS FETCH NEXT {PageSize} ROWS ONLY";

                var tasks = await _context.Tasks.FromSqlRaw(tasksQuery)
                    .ToListAsync(cancellationToken: cancellationToken);

                var totalCount = await _context.Tasks.CountAsync(cancellationToken);

                var taskPagedResult = PagedResult<Tasks>.Create(tasks,
                    PageIndex,
                    PageSize,
                    totalCount);

                var result = _mapper.Map<PagedResult<Response.TaskResponse>>(taskPagedResult);

                return Result.Success(result);
            }
            else // =>> Entity Framework
            {
                var tasksQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? _taskRepository.FindAll()
                : _taskRepository.FindAll(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));

                tasksQuery = request.SortOrder == SortOrder.Descending
                ? tasksQuery.OrderByDescending(GetSortProperty(request))
                : tasksQuery.OrderBy(GetSortProperty(request));

                var products = await PagedResult<Tasks>.CreateAsync(tasksQuery,
                    request.PageIndex,
                    request.PageSize);

                var result = _mapper.Map<PagedResult<Response.TaskResponse>>(products);
                return Result.Success(result);
            }
        }

        private static Expression<Func<Tasks, object>> GetSortProperty(Queries.GetTaskQuery request)
         => request.SortColumn?.ToLower() switch
         {
             "name" => task => task.Name,
             "description" => task => task.Description,
             _ => task => task.CreatedDate
         };
    }
}
