using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Models
{
    public class RefreshEntity
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Type { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }

    }
}
