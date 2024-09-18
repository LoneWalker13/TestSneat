using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Lessons.Command
{
  public class AddLessonCommand : IRequest<Result>
  {
    public string Name { get; set; }
    public decimal Price { get; set; }

    public class AddLessonCommandHandler : IRequestHandler<AddLessonCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public AddLessonCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(AddLessonCommand request, CancellationToken cancellationToken)
      {

          var lesson = _mapper.Map<BusinessCourse_Core.Entities.Lessons>(request);
          lesson.Status = LessonsStatus.Active;
          _context.Lessons.Add(lesson);
          await _context.SaveChangesAsync(cancellationToken);

          return new Result(true, new List<string>());

      }
    }
  }
}
