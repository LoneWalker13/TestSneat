using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessCourse.Models.Members.Members
{
  public class MemberViewModel
  {
    public BusinessCourse_Core.Entities.Members Member { get; set; }
    public List<SelectListItem> MembershipListItem { get; set; }
  }
}
