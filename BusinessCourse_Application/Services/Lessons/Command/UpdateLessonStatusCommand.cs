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

namespace BusinessCourse_Application.Services.Lessons.Command
{
  public class UpdateLessonStatusCommand : IRequest<Result>
  {
    public int LessonId { get; set; }
    public LessonsStatus Status { get; set; }
    public class UpdateLessonStatusCommandHandler : IRequestHandler<UpdateLessonStatusCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateLessonStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(UpdateLessonStatusCommand request, CancellationToken cancellationToken)
      {
        var lesson = _context.Lessons.First(x => x.Id == request.LessonId);
        lesson.Status = request.Status;
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
