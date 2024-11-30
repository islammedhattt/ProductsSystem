using Domain.Common;

namespace Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; } = 0;
        public string? Description { get; set; }
    }
}
