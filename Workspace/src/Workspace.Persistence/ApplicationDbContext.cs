using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workspace.Domain;
using Action = Workspace.Domain.Action;

namespace Workspace.Persistence
{
    public sealed class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        public DbSet<User> Users {  get; set; }

        public DbSet<Action> Actions { get; set; }

        public DbSet<Function> Functions { get; set; }

        public DbSet<ActionInFunction> ActionInFunctions { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Tasks> Tasks { get; set; }
    }
}
