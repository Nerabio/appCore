using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services
{
    public class DeviceService: IDeviceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Device> GetDevices()
        {
            return _unitOfWork
                .GetRepository<Device>()
                //.FindAll(x => x.IsActive)
                .All()
                .ToList();
        }

        public Device GetDevice(int deviceId)
        {
            return _unitOfWork
                .GetRepository<Device>()
                .Find(d => d.Id == deviceId);
        }

        public void DeviceTurnOff(int deviceId) 
        {
            var device = this.GetDevice(deviceId);
            device.IsActive = false;
            _unitOfWork.GetRepository<Device>().Update(device);
            _unitOfWork.SaveChanges();
        }

        public void DeviceTurnOn(int deviceId)
        {
            var device = this.GetDevice(deviceId);
            device.IsActive = true;
            _unitOfWork.GetRepository<Device>().Update(device);
            _unitOfWork.SaveChanges();
        }


        public Device getDeviceByGuid(string guidStr)
        {
            return _unitOfWork
                .GetRepository<Device>()
                .Find(d => d.Guid == guidStr);
        }

        private Device CreateDevice(Device device)
        {
            device.IsActive = true;
            device.IsConnected = true;
            _unitOfWork.GetRepository<Device>().Add(device);
            _unitOfWork.SaveChanges();
            return device;
        }

        private Device UpdateDevice(Device oldDevice, Device newDevice)
        {
            if (oldDevice.Name != newDevice.Name) oldDevice.Name = newDevice.Name;

            if (newDevice.SectionKey.Any())
            {
                foreach (var section in newDevice.SectionKey) {
                    var oldSk = oldDevice.SectionKey.FirstOrDefault(sk => sk.Name == section.Name);
                    if (oldSk != null)
                    {                    
                        foreach (var newKey in section.Keys)
                        {
                            var oldKey =  oldSk.Keys.FirstOrDefault(k => k.Name == newKey.Name);
                            if (oldKey != null)
                            {
                                oldKey.TypeKeyId = newKey.TypeKeyId;
                                oldKey.TypeKeyValueId = newKey.TypeKeyValueId;
                                oldKey.SetValue(newKey.GetValue());
                            }
                            else{
                                oldSk.Keys.Add(newKey);
                            }
                        }
                    }
                    else 
                    {
                        oldDevice.SectionKey.Add(section);
                    }
                }

            }
            _unitOfWork.GetRepository<Device>().Update(oldDevice);
            _unitOfWork.SaveChanges();
            return oldDevice;
        }

        public void CreateOrUpdateDevice(Device newDevice)
        {
            var oldDevice = this.getDeviceByGuid(newDevice.Guid);
            if (oldDevice == null)
            {
                this.CreateDevice(newDevice);
            }
            else{
                this.UpdateDevice(oldDevice, newDevice);
            }

        }


    }
}
