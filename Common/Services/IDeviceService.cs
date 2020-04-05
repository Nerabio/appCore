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
        void DeviceTurnOff(int deviceId);
        void DeviceTurnOn(int deviceId);
        void CreateOrUpdateDevice(Device device);
        Device getDeviceByGuid(string guidStr);
    }
}
