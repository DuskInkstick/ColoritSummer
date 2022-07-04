using ColoritSummer.Data.MySQL.Entities.Base;

namespace ColoritSummer.Data.MySQL.Entities
{
    public class User : NamedEntity
    {
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
