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

        protected List<SectionKey> changedSectionsKey = new List<SectionKey>();

        public KeyService(IUnitOfWork unitOfWork, ITaskService taskService)
        {
            _unitOfWork = unitOfWork;
            _taskService = taskService;
        }

        public void KeyUpdate(int deviceId, string[] vals) 
        {
            var relationRepo = _unitOfWork.GetRepository<DeviceRelation>();
            var keyRepo = _unitOfWork.GetRepository<Key>();
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
                        if (section != null)
                        {
                            changedSectionsKey.Add(section);

                            var key = section.Keys.FirstOrDefault(k => k.Name == keyName);
                            if (key != null)
                            {
                                key.SetValue(value);
                                _unitOfWork.GetRepository<Key>().Update(key);
                                //Проверяем связан ли этот ключ
                                var relationKeys = relationRepo.FindAll(r => r.KeyOutId == key.Id).ToList();
                                //обновляем связанный ключи если они есть
                                if (relationKeys.Any()) {
                                    foreach (var devKey in relationKeys)
                                    {
                                        var upKey = keyRepo.Get(devKey.KeyInId);
                                        upKey.SetValue(key.GetValue());
                                        changedSectionsKey.Add(upKey.SectionKey);
                                        _unitOfWork.GetRepository<Key>().Update(upKey);
                                    }
                                }

                            }
                        }
                    }
                }
            }
            _unitOfWork.SaveChanges();
            //_taskService.CreateTask();
            
            var uniqListSectionsKey = changedSectionsKey.Distinct().ToList();
            _taskService.CreateTaskBySections(uniqListSectionsKey);
        }


    }
}
