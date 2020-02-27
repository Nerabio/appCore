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
        private readonly IMapper _mapper;

        public DeviceController(IDeviceService deviceService, IMapper mapper)
        {
            _deviceService = deviceService;
            _mapper = mapper;
        }

        // GET: api/Device
        [HttpGet]
        public IList<DeviceViewModel> Get()
        {
            var devicesList = _deviceService.GetDevices();
            return devicesList.Select(d => _mapper.Map<DeviceViewModel>(d)).ToList();

        }

        // GET: api/Device/5
        [HttpGet("{id}", Name = "Get")]
        public DeviceViewModel Get(int id)
        {
            var device =  _deviceService.GetDevice(id);
            var dvm = _mapper.Map<DeviceViewModel>(device);
            return dvm;
        }

        // POST: api/Device
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Device/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
