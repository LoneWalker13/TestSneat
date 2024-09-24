using AutoMapper;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusinessCourse.Controllers
{
  public class LessonsController : MediatRController
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    //private readonly IDateTimeService _dateTimeService;
    public LessonsController(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
      //var breadcrumbs = new List<BreadcrumbItem>
      //      {
      //          new BreadcrumbItem { Title = "Home", Url = Url.Action("Index", "Home"), IsActive = false },
      //          new BreadcrumbItem { Title = "Bonus", Url = "#", IsActive = false },
      //          new BreadcrumbItem { Title = "Bonus List", Url = Url.Action("Index", "Bonus"), IsActive = true }
      //      };
      //ViewBag.Breadcrumbs = breadcrumbs;
      return View("Lessons/Index");
    }

    public async Task<IActionResult>  GetLessonsList(LessonsStatus status)
    {
      var lessons = await Mediator.Send(new GetLessonsQuery());
      if ((int)status != 2)
        lessons = lessons.Where(x => x.Status == status).ToList();

      var JsonResult = JsonConvert.SerializeObject(new { aaData = lessons });

      return Json(JsonResult);
    }


    [HttpGet]
    public async Task<IActionResult> AddLessons()
    {
      //ViewBag.IsMW = _currentTenantService.IsMW;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;

      var model = new AddLessonCommand();
      return PartialView("Lessons/_LessonsAdd", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddLessons(AddLessonCommand request)
    {
      //request.CreatedUser = "user";
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        return PartialView("Lessons/_LessonsAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }

    [HttpGet]
    public async Task<IActionResult> EditLessons(int lessonsId)
    {
      //ViewBag.IsMW = _currentTenantService.IsMW;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;

      var lesson = await Mediator.Send(new GetLessonsByIdQuery() {LessonsId = lessonsId });

      var model = _mapper.Map(lesson, new UpdateLessonsCommand());
      return PartialView("Lessons/_LessonsEdit", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditLessons(UpdateLessonsCommand command)
    {
      //command.LastUpdateUser = "user";
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        return PartialView("Bonus/_BonusUpdate", command);
      }
      var response = await Mediator.Send(command);
      return Json(new
      {
        result = response,
      });
    }


    [HttpPost]
    public async Task<JsonResult> UpdateLessonStatus(int lessonId, LessonsStatus status)
    {
      var response = await Mediator.Send(new UpdateLessonStatusCommand { LessonId = lessonId, Status = status });
      return Json(new
      {
        result = response,
      });
    }

  }
}
