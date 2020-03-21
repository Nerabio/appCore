using AutoMapper;
using DataAccess.Entities;
using DataAccess.Enums;
using Newtonsoft.Json;
using SmartHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SmartHouse.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();

            CreateMap<Device, DeviceViewModel>();

            CreateMap<SectionKey, SectionKeyViewModel>();

            CreateMap<Key, KeyViewModel>()
                .ForMember(x => x.TypeKey, opt => opt.MapFrom(src => this.GetTypeKey(src.TypeKeyId)))
                .ForMember(x => x.TypeKeyValue, opt => opt.MapFrom(src => this.GetTypeKeyValue(src.TypeKeyValueId)))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.GetValue()));

            CreateMap<DeviceRelation, DeviceRelationsModel>()
                .ForMember(x => x.KeyInName, opt => opt.MapFrom(src => src.KeyIn.Name))
                .ForMember(x => x.KeyOutName, opt => opt.MapFrom(src => src.KeyOut.Name))
                .ForMember(x => x.DeviceInName, opt => opt.MapFrom(src => src.DeviceIn.Name))
                .ForMember(x => x.DeviceOutName, opt => opt.MapFrom(src => src.DeviceOut.Name))
                .ForMember(x => x.DeviceInIdIsActive, opt => opt.MapFrom(src => src.DeviceIn.IsActive))
                .ForMember(x => x.DeviceOutIsActive, opt => opt.MapFrom(src => src.DeviceOut.IsActive));

            CreateMap<DeviceRelationsModel, DeviceRelation>();

            CreateMap<Task, TaskViewModel>();
        }

        private string GetTypeKey(int typeKeyId) 
        {
            switch (typeKeyId) 
            {
                case (int)TypeKeyEnum.Out: return "out";
                case (int)TypeKeyEnum.In: return "in";
                default: return "unknown";
            }
        }

        private string GetTypeKeyValue(int typeKeyValueId)
        {
            switch (typeKeyValueId)
            {
                case (int)TypeKeyValueEnum.String: return "string";
                case (int)TypeKeyValueEnum.Integer: return "integer";
                case (int)TypeKeyValueEnum.Boolean: return "boolean";
                default: return "unknown";
            }
        }

        //private string TaskToJson(Task task) 
        //{
        //    string json = JsonConvert.SerializeObject(product);
        //}

    }
}
