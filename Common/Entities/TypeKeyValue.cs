﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class TypeKeyValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Key Key { get; set; }
    }
}