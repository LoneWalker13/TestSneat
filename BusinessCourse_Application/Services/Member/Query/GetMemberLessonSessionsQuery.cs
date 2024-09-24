using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Member.Query
{
  public class GetMemberLessonSessionsQuery : IRequest<List<Member_LessonSessions>>
  {
    public int MemberId { get; set; }
    public int LessonId { get; set; }
  }

  public class GetMemberLessonSessionsQueryHandler : IRequestHandler<GetMemberLessonSessionsQuery, List<Member_LessonSessions>>
  {
    private readonly IMember _member;

    public GetMemberLessonSessionsQueryHandler(IMember member)
    {
      _member = member;
    }

    public async Task<List<Member_LessonSessions>> Handle(GetMemberLessonSessionsQuery request, CancellationToken cancellationToken)
    {
      var member = await _member.GetMember_LessonSessions(request.MemberId,request.LessonId);

      return member;
    }
  }
}
