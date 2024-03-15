namespace Workspace.Domain
{
    public class Project : EntityAuditBase<Guid>
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid ManagerId { get; set; }

        public int? Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
