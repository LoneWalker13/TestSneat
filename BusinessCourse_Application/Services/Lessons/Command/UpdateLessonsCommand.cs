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
  public class UpdateLessonsCommand : IRequest<Result>
  {
    public int LessonId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonsCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateLessonCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(UpdateLessonsCommand request, CancellationToken cancellationToken)
      {
        var lesson = _context.Lessons.First(x => x.Id == request.LessonId);
        _mapper.Map(request,lesson);
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
