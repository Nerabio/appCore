using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Models
{
    public class DeviceRelationsModel
    {
        public int Id { get; set; }
        public int DeviceOutId { get; set; }
        public string DeviceOutName { get; set; }
        public bool DeviceOutIsActive { get; set; }
        public int KeyOutId { get; set; }
        public string KeyOutName { get; set; }
        public int DeviceInId { get; set; }
        public string DeviceInName { get; set; }
        public bool DeviceInIdIsActive { get; set; }
        public int KeyInId { get; set; }
        public string KeyInName { get; set; }
    }
}
