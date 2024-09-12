using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class IconsController : Controller
{
  public IActionResult Boxicons() => View();
}
