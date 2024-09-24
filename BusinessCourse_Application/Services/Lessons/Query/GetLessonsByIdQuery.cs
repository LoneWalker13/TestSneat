using BusinessCourse_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Lessons.Query
{
  public class GetLessonsByIdQuery : IRequest<BusinessCourse_Core.Entities.Lessons>
  {
    public int LessonsId { get; set; }
  }

  public class GetLessonsByIdQueryHandler : IRequestHandler<GetLessonsByIdQuery, BusinessCourse_Core.Entities.Lessons>
  {
    private readonly ILessons _lessons;

    public GetLessonsByIdQueryHandler(ILessons lessons)
    {
      _lessons = lessons;
    }

    public async Task<BusinessCourse_Core.Entities.Lessons> Handle(GetLessonsByIdQuery request, CancellationToken cancellationToken)
    {
      var lesson = await _lessons.GetLessonsById(request.LessonsId);

      return lesson;
    }
  }
}
