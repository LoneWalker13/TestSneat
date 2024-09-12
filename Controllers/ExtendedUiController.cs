using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class ExtendedUiController : Controller
{
  public IActionResult PerfectScrollbar() => View();
  public IActionResult TextDivider() => View();
}
