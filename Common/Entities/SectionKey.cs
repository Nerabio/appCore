﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities
{
    public class SectionKey
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public string Name { get; set; }
        public IList<Key> Keys { get; set; }
    }
}