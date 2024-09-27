using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Query
{
  public class GetMemberLessonsByIdQuery : IRequest<Member_LessonSessions>
  {
    public int MemberLessonsId { get; set; }
  }

  public class GetMemberLessonsByIdQueryHandler : IRequestHandler<GetMemberLessonsByIdQuery,Member_LessonSessions>
  {
    private readonly IMemberLessonSessions _memberLessonSessions;

    public GetMemberLessonsByIdQueryHandler(IMemberLessonSessions memberLessonSessions)
    {
      _memberLessonSessions = memberLessonSessions;
    }

    public async Task<Member_LessonSessions> Handle(GetMemberLessonsByIdQuery request, CancellationToken cancellationToken)
    {
      var memberLessonSessions = await _memberLessonSessions.GetMemberLessonSessionsById(request.MemberLessonsId);

      return memberLessonSessions;
    }
  }
}
