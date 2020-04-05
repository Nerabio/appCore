using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class Key
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionKeyId { get; set; }
        public virtual SectionKey SectionKey { get; set; }
        public int TypeKeyValueId { get; set; }
        public virtual TypeKeyValue TypeKeyValue { get; set; }
        public int TypeKeyId { get; set; }
        public virtual TypeKey TypeKey { get; set; }
        public string ValueString { get; set; }
        public int? ValueInteger { get; set; }
        public bool? ValueBoolean { get; set; }
        public string Description { get; set; }

        [Timestamp]
        public byte[] TimeStamp { get; set; }

        public DateTime DateCreated { get; set; }
        public virtual IList<DeviceRelation> RelationOut { get; set; }
        public virtual IList<DeviceRelation> RelationIn { get; set; }
        public string GetValue()
        {
            switch (this.TypeKeyValueId) {
                case (int) TypeKeyValueEnum.String: return this.ValueString;
                case (int) TypeKeyValueEnum.Integer: return this.ValueInteger.ToString();
                case (int) TypeKeyValueEnum.Boolean: return this.ValueBoolean.ToString();
                default: return string.Empty;
            }
        }

        public void SetValue(string value) 
        {
            this.DateCreated = DateTime.Now;
            switch (this.TypeKeyValueId)
            {
                case (int)TypeKeyValueEnum.String:
                    this.ValueString = value as String;
                    break;
                case (int)TypeKeyValueEnum.Integer:
                    this.ValueInteger = Convert.ToInt32(value);
                    break;
                case (int)TypeKeyValueEnum.Boolean:
                    this.ValueBoolean = Boolean.Parse(value);
                    break;
                default: throw new Exception("Key (TypeKeyValueId) => SetValue: failed");
            }
        }
    }
}
