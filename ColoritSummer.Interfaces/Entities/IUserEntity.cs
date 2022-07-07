using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ColoritSummer.Interfaces.Entities
{
    public interface IUserEntity : IEntity
    {
        [Required]
        string Email { get; }

        [Required]
        string Password { get; }
    }
}
