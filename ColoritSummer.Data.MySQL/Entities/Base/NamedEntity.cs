using ColoritSummer.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoritSummer.Data.MySQL.Entities.Base
{
    public class NamedEntity : Entity, INamedEntity
    {
        public string Name { get; set; }
    }
}
