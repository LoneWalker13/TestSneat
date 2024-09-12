using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class FormLayoutsController : Controller
{
public IActionResult Horizontal() => View();
public IActionResult Vertical() => View();
}
