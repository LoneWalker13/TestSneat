using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Command
{
  public class UpdateMemberLessonsAttendanceCommand : IRequest<Result>
  {
    public int MemberLessonsId { get; set; }
    public Member_LessonSessionsAttendanceStatus AttendanceStatus { get; set; }
  }

  public class UpdateMemberLessonsAttendanceCommandHandler : IRequestHandler<UpdateMemberLessonsAttendanceCommand, Result>
  {
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMemberLessonsAttendanceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result> Handle(UpdateMemberLessonsAttendanceCommand request, CancellationToken cancellationToken)
    {
      try
      {

          var memberLessons = _context.MemberLessonSessions.FirstOrDefault(x => x.Id == request.MemberLessonsId);
          memberLessons.AttendStatus = request.AttendanceStatus;
          _context.MemberLessonSessions.Update(memberLessons);
          await _context.SaveChangesAsync(cancellationToken);
      }
      catch (Exception ex)
      {
        return new Result(false, new List<string>() { ex.Message });
      }


      return new Result(true, new List<string>() { });
    }
  }
}
