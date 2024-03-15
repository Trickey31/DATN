
namespace Workspace.Domain
{
    public abstract class EntityAuditBase<T> : DomainEntity<T>, IEntityAuditBase<T>
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int IsDelete { get; set; }
    }
}
