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
  public class GetMemberLessonsQuery : IRequest<List<GetMemberLessonSessionsList>>
  {
    public int LessonId { get; set; }
    public int LessonSessionsId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public Member_LessonSessionsPaymentStatus PaymentStatus { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendanceStatus { get; set; }
  }

  public class GetMemberLessonsQueryHandler : IRequestHandler<GetMemberLessonsQuery, List<GetMemberLessonSessionsList>>
  {
    private readonly IMemberLessonSessions _memberLessonSessions;

    public GetMemberLessonsQueryHandler(IMemberLessonSessions memberLessonSessions)
    {
      _memberLessonSessions = memberLessonSessions;
    }

    public async Task<List<GetMemberLessonSessionsList>> Handle(GetMemberLessonsQuery request, CancellationToken cancellationToken)
    {
      var lessonSessions = _memberLessonSessions.GetMemberLessonSessionsList(request.PhoneNumber,request.Name, request.LessonId,request.LessonSessionsId, request.AttendanceStatus,request.PaymentStatus);

      return lessonSessions;
    }
  }
}
