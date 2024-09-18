using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Command
{
  public class UpdateMemberListAttendanceCommand : IRequest<Result>
  {

    public List<Member_LessonSessions> Member_LessonSessionsList { get; set; }

    public class UpdateMemberListAttendanceCommandHandler : IRequestHandler<UpdateMemberListAttendanceCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateMemberListAttendanceCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(UpdateMemberListAttendanceCommand request, CancellationToken cancellationToken)
      {
        try
        {

          if (request.Member_LessonSessionsList.Count > 0)
          {
            _context.MemberLessonSessions.UpdateRange(request.Member_LessonSessionsList);
            await _context.SaveChangesAsync(cancellationToken);
          }
        }
        catch (Exception ex)
        {
          return new Result(false, new List<string>() { ex.Message });
        }


        return new Result(true, new List<string>() { });
      }
    }
  }
}
