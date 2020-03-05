using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Common.Services
{
    public class TaskService: ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTask(Device device) 
        {
            var taskRepo = _unitOfWork.GetRepository<Task>();

            var sections = device.SectionKey.Select(sk => new { 
                maxKeyTimeStamp = sk.Keys.Max(k => k.TimeStamp),
                SectionId = sk.Id,
                NameSection = sk.Name,
                Keys = sk.Keys
            });

            foreach (var section in sections) { 
                var taskMaxTimeStamp = taskRepo.FindAll(t=>t.SectionKeyId == section.SectionId).Max(t => t.TimeStamp);
                //if(taskMaxTimeStamp > section.maxKeyTimeStamp)
            }
        }

    }
}
