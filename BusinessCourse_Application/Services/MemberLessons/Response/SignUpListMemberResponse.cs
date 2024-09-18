using BusinessCourse_Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Response
{
  public class SignUpListMemberResponse
  {
    public List<string> MemberList { get; set; }
    public Result Results { get; set; }
  }
}
