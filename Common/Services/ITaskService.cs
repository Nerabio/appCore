using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public interface ITaskService
    {
        void CreateTask(Device device);
    }
}
