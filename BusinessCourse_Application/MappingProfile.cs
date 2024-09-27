using AutoMapper;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.LessonSessions.Command;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Application.Services.MemberLessons.Command;
using BusinessCourse_Application.Services.Membership.Command;
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
      CreateMap<UpdateMemberCommand, Members>();
      CreateMap<AddLessonCommand, Lessons>();
      CreateMap<UpdateLessonsCommand, Lessons>();
      CreateMap<Lessons, UpdateLessonsCommand>();
      CreateMap<AddLessonSessionsCommand, LessonSessions>();
      CreateMap<UpdateLessonSessionsCommand, LessonSessions>();
      CreateMap<LessonSessions, UpdateLessonSessionsCommand>();
      CreateMap<AddMembershipCommand, Membership>();
      CreateMap<UpdateMembershipCommand, Membership>();
      CreateMap<Membership, UpdateMembershipCommand>();
      CreateMap<UpdateMemberLessonsCommand, Member_LessonSessions>();
      CreateMap<Member_LessonSessions, UpdateMemberLessonsCommand>();
    }
  }
}
