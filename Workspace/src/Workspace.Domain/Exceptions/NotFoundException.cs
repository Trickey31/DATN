namespace Workspace.Domain
{
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }
}
