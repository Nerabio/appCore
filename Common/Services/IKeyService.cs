using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public interface IKeyService
    {
        void KeyUpdate(int deviceId, string[] value);
    }
}
