using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BusinessCourse.Models;

namespace BusinessCourse.Controllers;

public class AuthController : Controller
{
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();
  public IActionResult RegisterBasic() => View();
}
