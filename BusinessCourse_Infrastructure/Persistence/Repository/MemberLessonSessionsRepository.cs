using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Repository
{
  public class MemberLessonSessionsRepository : EFRepsitory<Member_LessonSessions>, IMemberLessonSessions
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public MemberLessonSessionsRepository(IConfiguration configuration, ApplicationDbContext context) : base(context)
    {
      _configuration = configuration;
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<List<Member_LessonSessions>> GetMemberLessonSessionsList(int lessonSessionId , string name, Member_LessonSessionsPaymentStatus paymentStatus, Member_LessonSessionsAttendanceStatus attendanceStatus)
    {
      return new List<Member_LessonSessions>();
    }
  }
}
