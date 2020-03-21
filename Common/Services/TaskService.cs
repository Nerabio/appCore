using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using DataAccess.Enums;
using Newtonsoft.Json;

namespace Common.Services
{
    public class TaskService: ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Создаем задачи для всех изменений
        /// </summary>
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
                        //Value = string.Join(",", section.Keys.Select(k => k.Name + "-" + k.GetValue()).ToArray())
                        Value = JsonConvert.SerializeObject(section.Keys.Select( k => new KeyValuePair<string, string> ( k.Name, k.GetValue())))
                    };

                    taskRepo.Add(newTask);
                    _unitOfWork.SaveChanges();
                }
            }
        }


        public void CreateTaskBySections(List<SectionKey> sectionsKey) {

            var sections = sectionsKey.Select(sk => new {
                maxKeyTimeStamp = sk.Keys.FirstOrDefault()?.TimeStamp, //new byte[8],
                Device = sk.Device,
                SectionKey = sk,
                SectionId = sk.Id,
                NameSection = sk.Name,
                Keys = sk.Keys
            }).ToList();

            var taskRepo = _unitOfWork.GetRepository<Task>();

            foreach (var section in sections)
            {
                var taskMaxTimeStamp = taskRepo.FindAll(t => t.SectionKeyId == section.SectionId && t.TaskStatusId == 1).FirstOrDefault()?.TimeStamp;
                bool needNewTask = StructuralComparisons.StructuralComparer.Compare(section.maxKeyTimeStamp, taskMaxTimeStamp) > 0;

                if (needNewTask)
                {

                    var newTask = new Task
                    {
                        DeviceId = section.Device.Id,
                        SectionKeyId = section.SectionKey.Id,
                        TaskStatusId = (int)TaskStatusEnum.New,
                        //Value = string.Join(",", section.Keys.Select(k => k.Name + "-" + k.GetValue()).ToArray())
                        Value = JsonConvert.SerializeObject(section.Keys.Select(k => new KeyValuePair<string, string>(k.Name, k.GetValue())))
                    };

                    taskRepo.Add(newTask);
                    _unitOfWork.SaveChanges();
                }
            }

        }

        public IList<Task> GetTasksForDevice(int deviceId) 
        {
            var taskRepo = _unitOfWork.GetRepository<Task>();
            return taskRepo.FindAll(t => t.DeviceId == deviceId && t.TaskStatusId == (int)TaskStatusEnum.New).ToList();
        }

        public void CompleteTaskById(int taskId) 
        {
            var taskRepo = _unitOfWork.GetRepository<Task>();
            var task = taskRepo.Find(t => t.Id ==taskId);
            task.TaskStatusId = (int)TaskStatusEnum.Completed;
            taskRepo.Update(task);
            _unitOfWork.SaveChanges();
        }
    }
}
