using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Common;
using BusinessCourse_Core.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.LessonSessions.Command
{
  public class UpdateLessonSessionsCommand : IRequest<Result>
  {
    public int Id { get; set; }
    public DateTime SessionDate { get; set; }
    public LessonSessionsStatus Status { get; set; }

    public class UpdateLessonSessionsCommandHandler : IRequestHandler<UpdateLessonSessionsCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateLessonSessionsCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(UpdateLessonSessionsCommand request, CancellationToken cancellationToken)
      {
        var lessonSessions = _context.LessonSessions.First(x=>x.Id == request.Id);
        _mapper.Map(request, lessonSessions);
        lessonSessions.SessionDate = DateTimeHelper.ConvertToUtc(lessonSessions.SessionDate);
        _context.LessonSessions.Update(lessonSessions);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
