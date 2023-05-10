namespace Tienda.Presentation.Models
{
    public abstract class EntityCoreModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get;set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
    }

    public abstract class EntityCoreFilter
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAtMin { get; set; } = DateTime.UtcNow.AddMonths(-1);
        public DateTime CreatedAtMax { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public ushort PageIndex { get; set; }
        public ushort PageCount { get; set; }
        public ushort PageSize { get; set; }        
        public int Total { get; set; }
        public string OrderByField { get; set; } = "Id";
        public int CurrentPageFirstRegisterNumber => FirstRegister();
        public int CurrentPageLastRegisterNumber => LastRegister();

        private int FirstRegister()
        {
            return (Total > 0) ? ((PageIndex * PageSize) - PageSize + 1) : 0;
        }

        private int LastRegister()
        {
            if (Total < 1 || (PageIndex * PageSize) > Total)
            {
                return Total;
            }
            return PageIndex * PageSize;
        }
    }

}
