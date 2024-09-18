using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Member.Query;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Lessons.Query
{
  public class GetLessonsQuery : IRequest<List<BusinessCourse_Core.Entities.Lessons>>
  {
  }

  public class GetLessonsQueryHandler : IRequestHandler<GetLessonsQuery, List<BusinessCourse_Core.Entities.Lessons>>
  {
    private readonly ILessons _lessons;

    public GetLessonsQueryHandler(ILessons lessons)
    {
      _lessons = lessons;
    }

    public async Task<List<BusinessCourse_Core.Entities.Lessons>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
    {
      var lessonList = await _lessons.GetLessonsList();

      return lessonList;
    }
  }
}
