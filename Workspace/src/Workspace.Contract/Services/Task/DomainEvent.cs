namespace Workspace.Contract
{
    public static class DomainEvent
    {
        public record TaskCreated(Guid Id) : IDomainEvent;

        public record TaskUpdated(Guid Id) : IDomainEvent;

        public record TaskDeleted(Guid Id) : IDomainEvent;
    }
}
