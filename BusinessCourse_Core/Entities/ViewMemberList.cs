using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Entities
{
  public class ViewMemberList : AuditableEntity
  {
    public string ChineseName { get; set; }
    public string EnglishName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? MemberCode { get; set; }
    public string? Remark { get; set; }
    public int Status { get; set; }
    public string Rank { get; set; }
    public int MembershipId { get; set; }

    [NotMapped]
    public string CreatedStr { get; set; }
  }
}
