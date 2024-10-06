using BusinessCourse_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Interfaces
{
  public interface IMember
  {
    Task<Members> GetMemberById(int memberId);
    Task<List<ViewMemberList>> GetMemberList(string name, string phoneNumber, string memberCode, DateTime from, DateTime to, int rank);
    Task<List<MemberLessonSessions>> GetMember_LessonSessions(int memberId, int lessonId);

  }
}
