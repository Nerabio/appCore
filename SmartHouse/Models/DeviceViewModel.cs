using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Models
{
    public class DeviceViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsConnected { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public IList<SectionKeyViewModel> SectionKey { get; set; }
    }
}
