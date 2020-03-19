using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services
{
    public class RelationService: IRelationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DeviceRelation> GetAllRealstions()
        {
            return _unitOfWork.GetRepository<DeviceRelation>().All().ToList();
        }

        public DeviceRelation CreateRelation(DeviceRelation relation)
        {
            var rel = _unitOfWork.GetRepository<DeviceRelation>().Add(relation);
            _unitOfWork.SaveChanges();
            return rel;
        }

        public void DeleteRelationById(int id) 
        {
            var relRepo = _unitOfWork.GetRepository<DeviceRelation>();
            var relation = relRepo.Find(r => r.Id == id);
            if (relation == null) throw new NullReferenceException("Relation not found");
            relRepo.Remove(relation);
            _unitOfWork.SaveChanges();
        }

    }
}
