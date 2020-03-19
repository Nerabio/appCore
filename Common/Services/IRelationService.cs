using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public interface IRelationService
    {
        List<DeviceRelation> GetAllRealstions();
        DeviceRelation CreateRelation(DeviceRelation relation);
        void DeleteRelationById(int id);
    }
}
