using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Entities
{
    public class Commands
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string Name { get; set; }

    }
}
