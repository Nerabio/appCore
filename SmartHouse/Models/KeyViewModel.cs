﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Models
{
    public class KeyViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string TypeKey { get; set; }
        public string TypeKeyValue { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
