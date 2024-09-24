using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.LessonSessions.Query;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Query
{
  public class GetMemberLessonsQuery : IRequest<List<Member_LessonSessions>>
  {
    public int LessonSessionId { get; set; }
    public string Name { get; set; }
    public Member_LessonSessionsPaymentStatus PaymentStatus { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendanceStatus { get; set; }
  }

  public class GetMemberLessonsQueryHandler : IRequestHandler<GetMemberLessonsQuery, List<Member_LessonSessions>>
  {
    private readonly IMemberLessonSessions _memberLessonSessions;

    public GetMemberLessonsQueryHandler(IMemberLessonSessions memberLessonSessions)
    {
      _memberLessonSessions = memberLessonSessions;
    }

    public async Task<List<Member_LessonSessions>> Handle(GetMemberLessonsQuery request, CancellationToken cancellationToken)
    {
      var lessonSessions = await _memberLessonSessions.GetMemberLessonSessionsList(request.LessonSessionId,request.Name,request.PaymentStatus,request.AttendanceStatus);

      return lessonSessions;
    }
  }
}
