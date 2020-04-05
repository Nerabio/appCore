using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Services;
using DataAccess;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Models;

namespace SmartHouse.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        private readonly IDeviceService _deviceService;
        private readonly IKeyService _keyService;
        private readonly IMapper _mapper;

        public DeviceController(IDeviceService deviceService, IMapper mapper, IKeyService keyService)
        {
            _deviceService = deviceService;
            _mapper = mapper;
            _keyService = keyService;
        }

        // GET: api/Device
        [HttpGet]
        public IList<DeviceViewModel> Get()
        {
            var devicesList = _deviceService.GetDevices();
            //var model = devicesList.Select(d => _mapper.Map<DeviceViewModel>(d)).ToList();
            var model = _mapper.Map<DeviceViewModel[]>(devicesList).ToList();
            return model;
        }


        [HttpGet("/api/device/turnOff/{id}", Name = "Device_turnOff")]
        public void turnOff(int id)
        {
            _deviceService.DeviceTurnOff(id);        
        }

        [HttpGet("/api/device/turnOn/{id}", Name = "Device_turnOn")]
        public void turnOn(int id)
        {
            _deviceService.DeviceTurnOn(id);
        }


        // GET: api/Device/5
        [HttpGet("{id}", Name = "Get")]
        public DeviceViewModel Get(int id)
        {
            var device =  _deviceService.GetDevice(id);
            var dvm = _mapper.Map<DeviceViewModel>(device);
            return dvm;
        }

        [HttpGet("/api/device/{guidStr}/Input")]
        public void Input(string guidStr, [FromQuery(Name = "vals")] string[] vals)
        {
            //vals = new String[] { "generalKey:num:777", "Srction1:v1:333" };
            var device = _deviceService.getDeviceByGuid(guidStr);
            _keyService.KeyUpdate(device.Id, vals);
        }


        [HttpPost("/api/device/init")]
        public void Init(Device device)
        {
            _deviceService.CreateOrUpdateDevice(device);
        }


        [HttpGet("/api/device/getChangedKeys")]
        public IList<KeyViewModel> GetChangedKeys()
        {
            var keys = _keyService.GetChangedKeys();
            var modelKeys = _mapper.Map<KeyViewModel[]>(keys);
            return modelKeys;
        }

    }
}
