using BusinessCourse_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.LessonSessions.Query
{
  public class GetLessonSessionsByIdQuery : IRequest<BusinessCourse_Core.Entities.LessonSessions>
  {
    public int LessonSessionsId { get; set; }
  }

  public class GetLessonSessionsByIdQueryHandler : IRequestHandler<GetLessonSessionsByIdQuery, BusinessCourse_Core.Entities.LessonSessions>
  {
    private readonly ILessons _lessons;

    public GetLessonSessionsByIdQueryHandler(ILessons lessons)
    {
      _lessons = lessons;
    }

    public async Task<BusinessCourse_Core.Entities.LessonSessions> Handle(GetLessonSessionsByIdQuery request, CancellationToken cancellationToken)
    {
      var lessonSessions = await _lessons.GetLessonsSessionsById(request.LessonSessionsId);

      return lessonSessions;
    }
  }
}
