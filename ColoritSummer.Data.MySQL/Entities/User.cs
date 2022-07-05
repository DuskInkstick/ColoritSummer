using ColoritSummer.Data.MySQL.Entities.Base;

namespace ColoritSummer.Data.MySQL.Entities
{
    public class User : UserEntity
    {
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
