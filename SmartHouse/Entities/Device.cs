using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsConnected { get; set; }
        public int StatusId { get; set; }

        public int TypeId { get; set; }
        public virtual DeviceType Type { get; set; }
        public DateTime LastConection { get; set; }
    }
}
