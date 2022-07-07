using ColoritSummer.Data.MySQL.Entities.Base;

namespace ColoritSummer.Data.MySQL.Entities
{
    public class ProductCard : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
