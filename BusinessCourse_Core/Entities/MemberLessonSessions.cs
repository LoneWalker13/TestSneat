using BusinessCourse_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
  public class MemberLessonSessions : AuditableEntity
  {
    public int MembersId { get; set; }
    public int LessonsId { get; set; }
    public int LessonSessionsId { get; set; }
    public decimal? DepositAmount { get; set; }
    public string Remark { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendStatus { get; set; }
    public Member_LessonSessionsPaymentStatus PaymentStatus { get; set; }
  }
}
