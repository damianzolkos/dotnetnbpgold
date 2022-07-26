using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetnbpgold.db.Entities
{
    public interface IAddable
    {
        public DateTime AddedAt { get; set; }
    }
}