using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
  public class LessonSessions : AuditableEntity
  {
    public int LessonsId { get; set; }
    public DateTime SessionDate { get; set; }
  }
}
