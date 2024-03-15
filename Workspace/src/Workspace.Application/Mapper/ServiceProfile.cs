using AutoMapper;
using Workspace.Contract;
using Workspace.Domain;

namespace Workspace.Application
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() 
        {
            #region ============ MAP ENTITY TO RESPONSE ============

            CreateMap<Tasks, Response.TaskResponse>().ReverseMap();
            CreateMap<PagedResult<Tasks>, PagedResult<Response.TaskResponse>>().ReverseMap();

            #endregion

            #region ============ MAP COMMAND TO ENTITY ============

            CreateMap<CreateTaskCommand, Tasks>().ReverseMap();

            #endregion
        }
    }
}
