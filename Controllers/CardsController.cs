using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class CardsController : Controller
{
  public IActionResult Basic() => View();
}
