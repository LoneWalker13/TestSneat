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
    Task<List<Member_LessonSessions>> GetMemberLessonSessionsList(int lessonSessionId, string name, Member_LessonSessionsPaymentStatus paymentStatus, Member_LessonSessionsAttendanceStatus attendanceStatus);
  }
}
