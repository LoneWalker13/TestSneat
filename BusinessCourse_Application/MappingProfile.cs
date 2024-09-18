using AutoMapper;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      MapFunction();
    }


    public void MapFunction()
    {
      CreateMap<AddMemberCommand, Members>();
      CreateMap<AddLessonCommand, Lessons>();
    }
  }
}
