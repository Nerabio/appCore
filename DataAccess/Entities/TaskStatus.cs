using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class TaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Task Task { get; set; }
    }
}
