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
    Task<List<Members>> GetMemberList(string name, string phoneNumber, string memberCode);
    Task<List<Member_LessonSessions>> GetMember_LessonSessions(int memberId, int lessonId);

  }
}
