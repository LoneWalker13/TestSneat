using BusinessCourse_Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
    public class GetMemberLessonSessionsList : AuditableEntity
    {
    public int MembersId { get; set; }
    public int LessonsId { get; set; }
    public int LessonSessionsId { get; set; }
    public decimal? DepositAmount { get; set; }
    public string? Remark { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendStatus { get; set; }
    public Member_LessonSessionsPaymentStatus PaymentStatus { get; set; }
    public string ChineseName { get; set; }
    public string EnglishName { get; set; }
    public string PhoneNumber { get; set; }

    [NotMapped]
    public string CreatedStr { get; set; }
  }
}
