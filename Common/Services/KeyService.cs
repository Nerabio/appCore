using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services
{
    public class KeyService: IKeyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskService _taskService;

        public KeyService(IUnitOfWork unitOfWork, ITaskService taskService)
        {
            _unitOfWork = unitOfWork;
            _taskService = taskService;
        }

        public void KeyUpdate(int deviceId, string[] vals) 
        {
            

            var device = _unitOfWork.GetRepository<Device>().Get(deviceId);

            foreach (string val in vals) {
                string sectionKey = null; string keyName = null; string value = null;
                string[] param = val.Split(new char[] { ':' });
                sectionKey = param[0]; keyName = param[1]; value = param[2];

                if (device != null)
                {
                    var sectionKeyList = device.SectionKey;
                    if (sectionKeyList.Any())
                    {
                        var section = sectionKeyList.FirstOrDefault(sk => sk.Name == sectionKey);
                        if (section != null && section.Keys.Any())
                        {
                            var key = section.Keys.FirstOrDefault(k => k.Name == keyName);
                            if (key != null)
                            {
                                key.SetValue(value);
                                _unitOfWork.GetRepository<Key>().Update(key);
                                
                            }
                        }
                    }
                }
            }
            _unitOfWork.SaveChanges();
            _taskService.CreateTask(device);
        }


    }
}
