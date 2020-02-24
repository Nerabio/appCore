using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsConnected { get; set; }
        public string Name { get; set; }

        public virtual IList<SectionKey> SectionKey { get; set; }

    }
}
