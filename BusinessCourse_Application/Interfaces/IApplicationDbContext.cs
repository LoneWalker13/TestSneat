using BusinessCourse_Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Interfaces
{
  public interface IApplicationDbContext
  {
    DbSet<Members> Members { get; set; }
    DbSet<Lessons> Lessons { get; set; }
    DbSet<LessonSessions> LessonSessions { get; set; }
    DbSet<Member_LessonSessions> MemberLessonSessions { get; set; }
    DbSet<Membership> Membership { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
