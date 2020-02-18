using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Entities
{
    public class Keys
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int CommandId { get; set; }
        public virtual Commands Command { get; set; }
        public int TypeKeyId { get; set; }
        public virtual TypeKey Type { get; set; }
        public string Name { get; set; }
    }
}
