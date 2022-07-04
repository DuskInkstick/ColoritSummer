using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ColoritSummer.Interfaces.Entities
{
    public  interface INamedEntity : IEntity
    {
        [Required]
        string Name { get; }
    }
}
