using BusinessCourse_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Interfaces
{
  public interface ILessons
  {
    Task<List<Lessons>> GetLessonsList();
    Task<Lessons> GetLessonsById(int lessonsId);

    Task<LessonSessions> GetLessonsSessionsById(int lessonSessionsId);
    Task<List<LessonSessions>> GetLessonsSessionsByLessonsId(int lessonsId);
  }
}
