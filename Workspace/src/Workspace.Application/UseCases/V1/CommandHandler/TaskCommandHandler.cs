using AutoMapper;
using MediatR;
using Workspace.Contract;
using Workspace.Domain;
using Workspace.Persistence;

namespace Workspace.Application
{
    public class TaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly IRepositoryBase<Tasks, Guid> _taskRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;

        public TaskCommandHandler(IRepositoryBase<Tasks, Guid> taskRepository, 
                                  ApplicationDbContext context,
                                  IMapper mapper,
                                  IPublisher publisher) 
        {
            _taskRepository = taskRepository;
            _context = context;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Tasks>(request);

            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity.IsDelete = 0;

            _taskRepository.Add(entity);

            await _publisher.Publish(new DomainEvent.TaskCreated(entity.Id));

            return Result.Success();
        }
    }
}
