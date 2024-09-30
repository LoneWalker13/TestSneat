using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
  public class LessonSessions : AuditableEntity
  {
    public int LessonsId { get; set; }
    public DateTime SessionDate { get; set; }
    public int Status { get; set; }

    [NotMapped]
    public string SessionDateStr { get; set; }

    [NotMapped]
    public string CreatedStr { get; set; }
    [NotMapped]
    public string LastModifiedStr { get; set; }
  }
}
