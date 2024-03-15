using FluentValidation;

namespace Workspace.Contract
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
