using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using DataAccess.Enums;

namespace Common.Services
{
    public class TaskService: ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTask() 
        {
            var taskRepo = _unitOfWork.GetRepository<Task>();
            var sectionRepo = _unitOfWork.GetRepository<SectionKey>();

            var sections = sectionRepo.GetAll().Select(sk => new { 
                maxKeyTimeStamp = sk.Keys.Max(k => k.TimeStamp),
                Device = sk.Device,
                SectionKey = sk,
                SectionId = sk.Id,
                NameSection = sk.Name,
                Keys = sk.Keys
            }).ToList();

            foreach (var section in sections) { 
                var taskMaxTimeStamp = taskRepo.FindAll(t=>t.SectionKeyId == section.SectionId).Max(t => t.TimeStamp);
                bool needNewTask = StructuralComparisons.StructuralComparer.Compare(section.maxKeyTimeStamp, taskMaxTimeStamp) > 0;
                if (needNewTask) {

                    var newTask = new Task
                    {
                        DeviceId = section.Device.Id,
                        SectionKeyId = section.SectionKey.Id,
                        TaskStatusId = (int)TaskStatusEnum.New,
                        Value = string.Join(",", section.Keys.Select(k => k.Name + "-" + k.GetValue()).ToArray())
                    };

                    taskRepo.Add(newTask);
                    _unitOfWork.SaveChanges();
                }
            }
        }

    }
}
