using AutoMapper;
using DataAccess.Entities;
using SmartHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHouse.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            //CreateMap<User, UserDto>();
            //CreateMap<UserDto, User>();

            CreateMap<Device, DeviceViewModel>()
                .ForMember(x => x.SectionKey, opt => opt.MapFrom(src => src.SectionKey
                .Select(c => 
                new SectionKeyViewModel { 
                    Id = c.Id , 
                    Name = c.Name, 
                    Keys= c.Keys.Select(k => 
                    new KeyViewModel { 
                        Id = k.Id , 
                        Description = k.Description, 
                        TypeKey = k.TypeKey.Name, 
                        TypeKeyValue = k.TypeKeyValue.Name,
                        Value = k.GetValue()
                    }).ToList()
                }).ToList()));
        }
    }
}
