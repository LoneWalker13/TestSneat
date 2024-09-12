using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class TablesController : Controller
{
  public IActionResult Basic() => View();
}
