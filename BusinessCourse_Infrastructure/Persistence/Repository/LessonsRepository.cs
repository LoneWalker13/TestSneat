using BusinessCourse_Application.Common.Extensions;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Repository
{
  public class LessonsRepository : EFRepsitory<Lessons>, ILessons
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public LessonsRepository(IConfiguration configuration, ApplicationDbContext context) : base(context)
    {
      _configuration = configuration;
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }



    public async Task<List<Lessons>> GetLessonsList()
    {
      var entity = await _context.Lessons
       .OrderByDescending(x => x.Created)
       .ToListAsync();

      return entity;
    }

    public async Task<Lessons> GetLessonsById(int lessonsId)
    {
      var entity = await _context.Lessons
        .FirstOrDefaultAsync(x => x.Id == lessonsId);

      return entity;
    }


    public async Task<LessonSessions> GetLessonsSessionsById(int lessonSessionsId)
    {
      var entity = await _context.LessonSessions
       .FirstOrDefaultAsync(x => x.Id == lessonSessionsId);

      return entity;
    }
    public async Task<List<LessonSessions>> GetLessonsSessionsByLessonsId(int lessonsId)
    {
      var entity = await _context.LessonSessions
       .Where(x => x.LessonsId == lessonsId)
       .Where(x => x.Status < 2)
       .OrderByDescending(x => x.SessionDate) 
       .ToListAsync();

      return entity;
    }
  }
}
