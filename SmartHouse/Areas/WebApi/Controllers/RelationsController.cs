using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Services;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHouse.Models;

namespace SmartHouse.Areas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRelationService _relationService;

        public RelationsController(IMapper mapper, IRelationService relationService)
        {
            _mapper = mapper;
            _relationService = relationService;
        }
       
        [HttpGet("/api/Relations/List")]
        public DeviceRelationsModel[] GetAllRelations()
        {
            var relations = _relationService.GetAllRealstions();
            var model = _mapper.Map<DeviceRelationsModel[]>(relations);
            return model;
        }

        [HttpPost("/api/Relations/Create")]
        public void CreateRelation([FromBody] DeviceRelationsModel relation)
        {
            var deviceRelation = _mapper.Map<DeviceRelation>(relation);
            _relationService.CreateRelation(deviceRelation);
        }

        [HttpGet("/api/Relations/Delete/{id}")]
        public void DeleteRelationById(int id)
        {
           _relationService.DeleteRelationById(id);
        }

        // GET: api/Relations
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Relations/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Relations
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Relations/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
