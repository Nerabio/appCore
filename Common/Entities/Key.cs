using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class Key
    {
        public int Id { get; set; }
        public int SectionKeyId { get; set; }
        public virtual SectionKey SectionKey { get; set; }
        public int TypeKeyValueId { get; set; }
        public virtual TypeKeyValue TypeKeyValue { get; set; }
        public int TypeKeyId { get; set; }
        public virtual TypeKey TypeKey { get; set; }
        public string ValueString { get; set; }
        public int? ValueInteger { get; set; }
    }
}
