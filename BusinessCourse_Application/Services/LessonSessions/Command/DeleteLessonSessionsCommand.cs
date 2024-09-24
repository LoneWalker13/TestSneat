using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.LessonSessions.Command
{
  public class DeleteLessonSessionsCommand : IRequest<Result>
  {
    public int LessonSessionsId { get; set; }

    public class DeleteLessonSessionsCommandHandler : IRequestHandler<DeleteLessonSessionsCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public DeleteLessonSessionsCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(DeleteLessonSessionsCommand request, CancellationToken cancellationToken)
      {
        var lessonSessions = _context.LessonSessions.First(x=>x.Id == request.LessonSessionsId);
        lessonSessions.Status = 0;
        _context.LessonSessions.Update(lessonSessions);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
