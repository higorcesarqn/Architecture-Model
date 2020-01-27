using Core.Aggregate;
using Core.Models;

namespace Domain.AggregatesModel.PermissionAggregate
{
    public class Permission : Enumeration, IAggregateRoot
    {
        public Permission(int id, string name) : base(id, name) { }

        public Permission(string name) : base(0, name) { }

        public string Title { get; protected set; }
        public string Description { get; protected set; }
    }
}
