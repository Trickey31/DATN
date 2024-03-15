using FluentValidation;

namespace Workspace.Contract
{
    public class UpdateTaskValidator : AbstractValidator<Commands.UpdateTask>
    {
        public UpdateTaskValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Task.Name).NotEmpty();
        }
    }
}
