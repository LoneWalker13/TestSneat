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
    Task<Members> GetMemberByPhoneNumber(string phoneNumber);
    Task<List<Member_LessonSessions>> GetMember_LessonSessions(int memberId);

  }
}
