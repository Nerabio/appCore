using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }


        public string Value { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public int TaskStatusId { get; set; }
        public virtual TaskStatus TaskStatus { get; set; }

        public int SectionKeyId { get; set; }
        public virtual SectionKey SectionKey { get; set; }
    }
}
