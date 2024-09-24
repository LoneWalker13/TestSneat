using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
  public class Membership : AuditableEntity
  {
    public string Rank { get; set; }
    public int Tier { get; set; }
    public bool Status { get; set; }
  }
}
