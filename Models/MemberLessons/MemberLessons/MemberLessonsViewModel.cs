using BusinessCourse_Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessCourse.Models.MemberLessons.MemberLessons
{
  public class MemberLessonsViewModel
  {
    public List<SelectListItem> LessonsList { get; set; }
  }
}
