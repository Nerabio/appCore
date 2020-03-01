using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsConnected { get; set; }
        public string Name { get; set; }

        public virtual IList<SectionKey> SectionKey { get; set; }

        public virtual IList<DeviceRelation> RelationOut { get; set; }
        public virtual IList<DeviceRelation> RelationIn { get; set; }
    }
}
