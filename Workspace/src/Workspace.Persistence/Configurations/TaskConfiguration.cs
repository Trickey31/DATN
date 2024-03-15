using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks = Workspace.Domain.Tasks;

namespace Workspace.Persistence
{
    public sealed class TaskConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable(Constants.Task);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.Status).IsRequired(false);
            builder.Property(x => x.Deadline);
            builder.Property(x => x.ProjectId).IsRequired(true);
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.Priority);
            builder.Property(x => x.CreatedDate);
            builder.Property(x => x.UpdatedDate);
            builder.Property(x => x.IsDelete);
        }
    }
}
