using AutoMapper;
using e_robot.Application.Features.Commands;
using e_robot.Application.Features.DtoModels;
using e_robot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_robot.Application.Mappings
{
    partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterCommmand, User>(); 
            CreateMap<Solution, SolutionDto>(); 
            CreateMap<Exam, ExamDto>();
        }
    }
}
