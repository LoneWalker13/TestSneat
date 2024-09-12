using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class DashboardsController : Controller
{
  public IActionResult Index() => View();
}
