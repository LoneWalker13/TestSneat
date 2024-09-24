using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Repository
{
  public class MemberRepository : EFRepsitory<Members>, IMember
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public MemberRepository(IConfiguration configuration, ApplicationDbContext context) : base(context)
    {
      _configuration = configuration;
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }



    public async Task<List<Members>> GetMemberList(string name, string phoneNumber,string memberCode)
    {
      var entity = await _context.Members.
        FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

      return new List<Members>();
    }

    public async Task<List<Member_LessonSessions>> GetMember_LessonSessions(int memberId, int lessonId)
    {
      var entity = await _context.MemberLessonSessions
        .Where(x => x.MemberId == memberId && x.LessonsId == lessonId)
          .OrderByDescending(x=>x.Created)
          .ToListAsync();


      return entity;
    }

    public async Task<Members> GetMemberById(int memberId)
    {
      var entity = await _context.Members.FirstOrDefaultAsync(x=>x.Id == memberId);
      return entity;
    }
  }
}
