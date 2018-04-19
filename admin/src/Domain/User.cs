using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain
{
    public class User : Entity
    {
        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Avator { get; set; }

        public IList<Role> Roles { get; set; }
    }
}
