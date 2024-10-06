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
  public class GetMemberLessonsByIdQuery : IRequest<MemberLessonSessions>
  {
    public int MemberLessonsId { get; set; }
  }

  public class GetMemberLessonsByIdQueryHandler : IRequestHandler<GetMemberLessonsByIdQuery,MemberLessonSessions>
  {
    private readonly IMemberLessonSessions _memberLessonSessions;

    public GetMemberLessonsByIdQueryHandler(IMemberLessonSessions memberLessonSessions)
    {
      _memberLessonSessions = memberLessonSessions;
    }

    public async Task<MemberLessonSessions> Handle(GetMemberLessonsByIdQuery request, CancellationToken cancellationToken)
    {
      var memberLessonSessions = await _memberLessonSessions.GetMemberLessonSessionsById(request.MemberLessonsId);

      return memberLessonSessions;
    }
  }
}
