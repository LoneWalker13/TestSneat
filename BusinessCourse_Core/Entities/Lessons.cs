using BusinessCourse_Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
  public class Lessons : AuditableEntity
  {
    public string Name { get; set; }
    public LessonsStatus Status { get; set; }
    public decimal Price { get; set; }
    public List<LessonSessions> LessonsSessions { get; set; }

    [NotMapped]
    public string CreatedStr { get; set; }
    [NotMapped]
    public string LastModifiedStr { get; set; }
  }
}
