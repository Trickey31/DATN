﻿namespace Workspace.Contract
{
    public class TaskCommand : ICommand
    {
        public Guid ProjectId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Priority { get; set; }

        public int? Status { get; set; }

        public DateTime Deadline { get; set; }
    }
}
