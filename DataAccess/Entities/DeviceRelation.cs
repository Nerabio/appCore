using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class DeviceRelation
    {
        public int Id { get; set; }
        public int DeviceOutId { get; set; }
        public virtual Device DeviceOut { get; set; }
        public int KeyOutId { get; set; }
        public virtual Key KeyOut { get; set; }
        public int DeviceInId { get; set; }
        public virtual Device DeviceIn { get; set; }
        public int KeyInId { get; set; }
        public virtual Key KeyIn { get; set; }
    }
}
