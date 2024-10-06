using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Command
{
  public class SignUpMemberLessonsCommand : IRequest<Result>
  {
    public string ChineseName { get; set; }
    public string EnglishName { get; set; }
    public string PhoneNumber { get; set; }
    public int LessonsId { get; set; }
    public int LessonSessionsId { get; set; }
    public decimal? DepositAmount { get; set; } = null;
    public string? Remark { get; set; }
    public Member_LessonSessionsPaymentStatus PaymentStatus { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendStatus { get; set; }


    public class SignUpMemberLessonsCommandHandler : IRequestHandler<SignUpMemberLessonsCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public SignUpMemberLessonsCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(SignUpMemberLessonsCommand request, CancellationToken cancellationToken)
      {
        int memberId = 0;

        try
        {
          var existMember = _context.Members.FirstOrDefault(x => x.PhoneNumber.Equals(request.PhoneNumber.Trim()));

          var membershipTier0 = _context.Membership.First(x => x.Tier == 0 && x.Status);
          if (existMember == null)
          {
            var member = new Members()
            {
              ChineseName = request.ChineseName,
              EnglishName = request.EnglishName,
              PhoneNumber = request.PhoneNumber,
              Status = (int)MembersStatus.Active,
              MembershipId = membershipTier0.Id
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync(cancellationToken);

            memberId = member.Id;
          }
          else
            memberId = existMember.Id;


          var lesson = _context.Lessons.Include(x => x.LessonsSessions).FirstOrDefault(x => x.LessonsSessions.Any(y => y.Id == request.LessonSessionsId)) ?? throw new Exception("LessonsNotFound");

          var existMemberLessonSession = _context.MemberLessonSessions.FirstOrDefault(x=>x.MembersId == memberId && x.LessonSessionsId == request.LessonSessionsId);

          if (existMemberLessonSession != null)
            throw new Exception("SignedUpMember");

          var memberLessonSession = new MemberLessonSessions()
          {
            MembersId = memberId,
            LessonsId = lesson.Id,
            LessonSessionsId = request.LessonSessionsId,
            DepositAmount = request.DepositAmount,
            PaymentStatus = request.PaymentStatus,
            AttendStatus = request.AttendStatus,
            Remark = request.Remark
          };

          _context.MemberLessonSessions.Add(memberLessonSession);
          await _context.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {
          return new Result(false, new List<string>() {ex.Message});
        }


        return new Result(true, new List<string>() { });
      }
    }

  }
}
