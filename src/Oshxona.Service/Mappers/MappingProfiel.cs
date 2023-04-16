using AutoMapper;
using Oshxona.Domain.Entites;
using Oshxona.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Service.Mappers
{
    public class MappingProfiel : Profile
    {
        public MappingProfiel()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<Meal,MealDto>().ReverseMap();
        }
    }
}
