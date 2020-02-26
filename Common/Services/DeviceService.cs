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

    }
}
