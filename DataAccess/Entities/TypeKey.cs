﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class TypeKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Key Key { get; set; }
    }
}
