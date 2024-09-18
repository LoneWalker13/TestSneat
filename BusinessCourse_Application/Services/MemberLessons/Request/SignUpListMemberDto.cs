using BusinessCourse_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Request
{
  public class SignUpListMemberDto
  {
    public string ChineseName { get; set; }
    public string EnglishName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public decimal DepositAmount { get; set; } 
    public string Remark { get; set; }
    public Member_LessonSessionsPaymentStatus PaymentStatus { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendanceStatus { get; set; }
  }
}
