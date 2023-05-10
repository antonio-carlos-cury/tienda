using Tienda.Domain.Entities;

namespace Tienda.Domain.Core
{
    public interface IEntityFilter
    {
        int PageIndex { get; set; }
        int PageCount { get; }
        int PageSize { get; set; }
        int Total { get; set; }
    }

    public interface IEntityCore
    {
        DateTime CreatedAt { get; set; }
        Guid Id { get; set; }

        bool IsDeleted { get; set; }
    }


    public abstract class EntityCore : IEntityCore
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public EntityCore()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }

    public abstract class EntityFilter : IEntityFilter
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public Guid Id { get; set; }
        public string OrderByValue { get; set; }
    }
}
