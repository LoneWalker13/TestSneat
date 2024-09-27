using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.MemberLessons.Command
{
  public class DeleteMemberLessonsCommand : IRequest<Result>
  {
    public int MemberLessonsSessionId { get; set; }

    public class DeleteMemberLessonsCommandHandler : IRequestHandler<DeleteMemberLessonsCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public DeleteMemberLessonsCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(DeleteMemberLessonsCommand request, CancellationToken cancellationToken)
      {
        try
        {

          var memberLessons = _context.MemberLessonSessions.FirstOrDefault(x => x.Id == request.MemberLessonsSessionId);
          _context.MemberLessonSessions.Remove(memberLessons);
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
}
