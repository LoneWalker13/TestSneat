using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Interfaces
{
  public interface IMemberLessonSessions
  {
    Task<MemberLessonSessions> GetMemberLessonSessionsById ( int memberLessionSessionId);
    List<GetMemberLessonSessionsList> GetMemberLessonSessionsList(string phoneNumber,string name, int lessonsId, int lessonSessionsId, Member_LessonSessionsAttendanceStatus attendanceStatus, Member_LessonSessionsPaymentStatus paymentStatus);
  }
}
