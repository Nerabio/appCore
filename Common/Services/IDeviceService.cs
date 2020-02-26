using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public interface IDeviceService
    {
        IList<Device> GetDevices();
        Device GetDevice(int deviceId);
    }
}
