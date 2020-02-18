using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Entities
{
    public class Values
    {
        public int Id { get; set; }
        public int KeyId { get; set; }
        public virtual Keys Key { get; set; }
        public int ValueInt { get; set; }
        public string ValueStr { get; set; }
        public bool ValueBool { get; set; }
    }
}
