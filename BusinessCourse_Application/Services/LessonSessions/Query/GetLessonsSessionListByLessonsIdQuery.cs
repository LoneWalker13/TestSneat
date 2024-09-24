using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Lessons.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.LessonSessions.Query
{
  public class GetLessonsSessionListByLessonsIdQuery : IRequest<List<BusinessCourse_Core.Entities.LessonSessions>>
  {
    public int LessonId { get; set; }
  }

  public class GetLessonsSessionListQueryHandler : IRequestHandler<GetLessonsSessionListByLessonsIdQuery, List<BusinessCourse_Core.Entities.LessonSessions>>
  {
    private readonly ILessons _lessons;

    public GetLessonsSessionListQueryHandler(ILessons lessons)
    {
      _lessons = lessons;
    }

    public async Task<List<BusinessCourse_Core.Entities.LessonSessions>> Handle(GetLessonsSessionListByLessonsIdQuery request, CancellationToken cancellationToken)
    {
      var lessonSessions = await _lessons.GetLessonsSessionsByLessonsId(request.LessonId);

      return lessonSessions;
    }
  }
}
