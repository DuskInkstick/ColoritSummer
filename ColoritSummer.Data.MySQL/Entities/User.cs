using ColoritSummer.Data.MySQL.Entities.Base;

namespace ColoritSummer.Data.MySQL.Entities
{
    public class User : UserEntity
    {
        public string Description { get; set; }
        public string OrganizationName { get; set; }
        public string City { get; set; }
        public bool IsBanned { get; set; }
    }
}
