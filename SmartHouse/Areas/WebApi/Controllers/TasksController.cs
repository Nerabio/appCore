using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Models;

namespace SmartHouse.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IKeyService _keyService;
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TasksController(IDeviceService deviceService, IMapper mapper, IKeyService keyService, ITaskService taskService)
        {
            _deviceService = deviceService;
            _mapper = mapper;
            _keyService = keyService;
            _taskService = taskService;
        }

        [HttpGet("/api/tasks/get/{deviceId}")]
        public IList<TaskViewModel> GetTasks(int deviceId)
        {
            var tasks = _taskService.GetTasksForDevice(deviceId);
            var model = _mapper.Map<TaskViewModel[]>(tasks).ToList();
            return model;
        }

        [HttpGet("/api/tasks/complete/{taskId}")]
        public void CompleteTask(int taskId)
        {
            _taskService.CompleteTaskById(taskId);
        }

    }
}
