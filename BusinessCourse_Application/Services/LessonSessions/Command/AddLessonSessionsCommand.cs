using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.LessonSessions.Command
{
  public class AddLessonSessionsCommand : IRequest<Result>
  {
    public int LessionId { get; set; }
    public DateTime SessionDate { get; set; }


    public class AddLessonsSessionCommandHandler : IRequestHandler<AddLessonSessionsCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public AddLessonsSessionCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(AddLessonSessionsCommand request, CancellationToken cancellationToken)
      {
        var lessonSessions = _mapper.Map<BusinessCourse_Core.Entities.LessonSessions>(request);
        _context.LessonSessions.Add(lessonSessions);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
