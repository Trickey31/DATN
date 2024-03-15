using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workspace.Contract;

namespace Workspace.Presentation
{
    [ApiVersion(1)]
    public class TaskController : ApiController
    {
        public TaskController(ISender sender) : base(sender)
        {
        }

        [HttpGet(Name = "GetTasks")]
        [ProducesResponseType(typeof(TResult<IEnumerable<Response.TaskResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTasks(string? searchTerm = null, 
            string? sortColumn = null, 
            string? sortOrder = null,
            string? sortColumnAndOrder = null,
            int pageIndex = 1, int pageSize = 10)
        {
            var result = 
                await Sender.Send(new Queries.GetTaskQuery(searchTerm, 
                sortColumn, SortOrderExtension.ConvertStringToSortOrder(sortOrder),
                SortOrderExtension.ConvertStringToSortColumnAndOrder(sortColumnAndOrder),
                pageIndex, pageSize));

            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(typeof(TResult<IEnumerable<Response.TaskResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var result = await Sender.Send(new Queries.GetTaskIdQuery(id));

            return Ok(result);
        }

        [HttpPost(Name = "CreateTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand createTask)
        {
            var result = await Sender.Send(createTask);

            if(result.IsFailed)
            {
                return HandlerFailure(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var result = await Sender.Send(new Commands.DeleteTask(id));

            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] Commands.UpdateTask updateTask)
        {
            var result = await Sender.Send(new Commands.UpdateTask(id, updateTask.Task));

            return Ok(result);
        }
    }
}
