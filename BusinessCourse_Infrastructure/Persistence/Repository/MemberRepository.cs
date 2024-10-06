using BusinessCourse_Application.Common.Extensions;
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



    public async Task<List<ViewMemberList>> GetMemberList(string name, string phoneNumber,string memberCode,DateTime from, DateTime to, int rank)
    {
      var entity = await _context.ViewMemberList
        .WhereIf(!string.IsNullOrEmpty(name), x => x.ChineseName.Contains(name) || x.EnglishName.Contains(name))
        .WhereIf(!string.IsNullOrEmpty(phoneNumber), x => x.PhoneNumber.Contains(phoneNumber))
        .WhereIf(!string.IsNullOrEmpty(memberCode), x => x.MemberCode.Contains(memberCode))
        .WhereIf(rank > 0 , x => x.MembershipId == rank)
        .WhereIf(string.IsNullOrEmpty(name) && string.IsNullOrEmpty(phoneNumber) && string.IsNullOrEmpty(memberCode) && rank == 0, x => x.Created >= from && x.Created <= to)
        .ToListAsync();

      return entity;
    }

    public async Task<List<MemberLessonSessions>> GetMember_LessonSessions(int memberId, int lessonId)
    {
      var entity = await _context.MemberLessonSessions
        .Where(x => x.MembersId == memberId && x.LessonsId == lessonId)
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
