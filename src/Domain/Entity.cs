using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Entity
    {
        public long Id { get; set; }

        [ConcurrencyCheck]
        public TimeSpan TimeStamp { get; set; }
    }
}
