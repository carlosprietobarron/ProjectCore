using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.courses;
using AutoMapper;
using Dominion;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ForMember(x => x.Teachers, y => y.MapFrom(z => z.TeacherLinks.Select(a => a.Teacher).ToList()))
                .ForMember(x => x.Comentaries, y => y.MapFrom(z => z.Comentaries))
                .ForMember(x => x.PriceDTO, y => y.MapFrom(z => z.SalesPrice));
            
            CreateMap<CourseTeacher, CourseTeacherDTO>();
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<Comentary, ComentaryDTO>();
            CreateMap<Price, PriceDTO>();
        }
    }
}