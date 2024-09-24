using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.MemberLessons.Request;
using BusinessCourse_Application.Services.MemberLessons.Response;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Command
{
  public class SignUpListMemberLessonsCommand : IRequest<SignUpListMemberResponse>
  {
    public IFormFile File { get; set; }
    public int LessonSessionsId { get; set; }


    public class SignUpListMemberLessonsCommandHandler : IRequestHandler<SignUpListMemberLessonsCommand, SignUpListMemberResponse>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public SignUpListMemberLessonsCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<SignUpListMemberResponse> Handle(SignUpListMemberLessonsCommand request, CancellationToken cancellationToken)
      {
        var existMemberLessonSessionPhoneNumber = new List<string>();

        try
        {
          var signUpListMembers = new List<SignUpListMemberDto>();
          var lesson = _context.Lessons.Include(x => x.LessonsSessions).FirstOrDefault(x => x.LessonsSessions.Any(y => y.Id == request.LessonSessionsId)) ?? throw new Exception("LessonsNotFound");

          using (var stream = request.File.OpenReadStream())
          using (var reader = new StreamReader(stream))
          using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
          {
            signUpListMembers = csv.GetRecords<SignUpListMemberDto>().ToList();
          }

          

          foreach(var signUpMember in signUpListMembers)
          {
            int memberId = 0;
            var existMember = _context.Members.FirstOrDefault(x => x.PhoneNumber.Equals(signUpMember.PhoneNumber.Trim()));
            if (existMember == null)
            {
              var member = new Members()
              {
                ChineseName = signUpMember.ChineseName,
                Email = signUpMember.Email,
                EnglishName = signUpMember.EnglishName,
                PhoneNumber = signUpMember.PhoneNumber,
                Status = (int)MembersStatus.Active
              };
              _context.Members.Add(member);
              await _context.SaveChangesAsync(cancellationToken);

              memberId = member.Id;
            }
            else
              memberId = existMember.Id;

            var existMemberLessonSession = _context.MemberLessonSessions.FirstOrDefault(x => x.MemberId == memberId && x.LessonSessionsId == request.LessonSessionsId);

            if (existMemberLessonSession != null)
            {
              existMemberLessonSessionPhoneNumber.Add(signUpMember.PhoneNumber);
              continue;
            }

            var memberLessonSession = new Member_LessonSessions()
            {
              MemberId = memberId,
              LessonsId = lesson.Id,
              LessonSessionsId = request.LessonSessionsId,
              DepositAmount = signUpMember.DepositAmount,
              PaymentStatus = signUpMember.PaymentStatus,
              AttendStatus = signUpMember.AttendanceStatus,
              Remark = signUpMember.Remark
            };

            _context.MemberLessonSessions.Add(memberLessonSession);
          }

          await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
          var result =  new Result(false, new List<string>() { ex.Message });
          return new SignUpListMemberResponse() { Results = result };
        }


        var result2 =  new Result(true, new List<string>() { });
        return new SignUpListMemberResponse() { Results = result2 , MemberList = existMemberLessonSessionPhoneNumber };
      }
    }
  }
}
