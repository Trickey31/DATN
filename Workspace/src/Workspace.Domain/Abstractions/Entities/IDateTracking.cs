namespace Workspace.Domain
{
    public interface IDateTracking
    {
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }
    }
}
