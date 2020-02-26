using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Models
{
    public class SectionKeyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<KeyViewModel> Keys { get; set; }
    }
}
