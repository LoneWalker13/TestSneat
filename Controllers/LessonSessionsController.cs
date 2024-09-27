using AutoMapper;
using BusinessCourse.Models.LessonSessions.LessonSessions;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.LessonSessions.Command;
using BusinessCourse_Application.Services.LessonSessions.Query;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BusinessCourse.Controllers
{
  public class LessonSessionsController : MediatRController
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    //private readonly IDateTimeService _dateTimeService;
    public LessonSessionsController(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {

      var lessons = await GetActiveLessons();
      var model = new LessonSessionsViewModel() { Lessons = lessons };  
      //var breadcrumbs = new List<BreadcrumbItem>
      //      {
      //          new BreadcrumbItem { Title = "Home", Url = Url.Action("Index", "Home"), IsActive = false },
      //          new BreadcrumbItem { Title = "Bonus", Url = "#", IsActive = false },
      //          new BreadcrumbItem { Title = "Bonus List", Url = Url.Action("Index", "Bonus"), IsActive = true }
      //      };
      //ViewBag.Breadcrumbs = breadcrumbs;
      return View("LessonSessions/Index",model);
    }


    public async Task<IActionResult> GetLessonsSessionsByLessonId(int lessonsId)
    {
      var lessonsSessions = await Mediator.Send(new GetLessonsSessionListByLessonsIdQuery() { LessonId = lessonsId });

      var JsonResult = JsonConvert.SerializeObject(new { aaData = lessonsSessions });

      return Json(JsonResult);
    }

    public async Task<List<Lessons>> GetActiveLessons()
    {
      var lessons = await Mediator.Send(new GetLessonsQuery());
      lessons = lessons.Where(x=>x.Status == LessonsStatus.Active).ToList();
      return lessons;
    }

    [HttpGet]
    public async Task<IActionResult> AddLessonSessions()
    {
      //ViewBag.IsMW = _currentTenantService.IsMW;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;

      List<SelectListItem> lessonsItems = new List<SelectListItem>();
      var lessons = await GetActiveLessons();
      lessonsItems.Add(new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true });
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
      ViewBag.LessonsList = lessonsItems;
      var model = new AddLessonSessionsCommand();
      return PartialView("LessonSessions/_LessonSessionsAdd", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddLessonSessions(AddLessonSessionsCommand request)
    {
      //request.CreatedUser = "user";
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        List<SelectListItem> lessonsItems = new List<SelectListItem>();
        var lessons = await GetActiveLessons();
        lessonsItems.Add(new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true });
        lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
        ViewBag.LessonsList = lessonsItems;
        return PartialView("Lessons/_LessonSessionsAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }


    [HttpGet]
    public async Task<IActionResult> EditLessonSessions(int lessonSessionsId)
    {
      //ViewBag.IsMW = _currentTenantService.IsMW;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;

      var lessonSession = await Mediator.Send(new GetLessonSessionsByIdQuery() { LessonSessionsId = lessonSessionsId });
      List<SelectListItem> lessonsItems = new List<SelectListItem>();
      var lessons = await GetActiveLessons();
      lessonsItems.Add(new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true });
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
      ViewBag.LessonsList = lessonsItems;
      var model = _mapper.Map(lessonSession, new UpdateLessonSessionsCommand());
      return PartialView("Lessons/_LessonsEdit", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditLessonSessions(UpdateLessonSessionsCommand command)
    {
      //command.LastUpdateUser = "user";
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        List<SelectListItem> lessonsItems = new List<SelectListItem>();
        var lessons = await GetActiveLessons();
        lessonsItems.Add(new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true });
        lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
        ViewBag.LessonsList = lessonsItems;
        return PartialView("Lessons/_LessonsEdit", command);
      }
      var response = await Mediator.Send(command);
      return Json(new
      {
        result = response,
      });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteLessonSessions(int lessonsSessionsId)
    {

      var response  = await Mediator.Send(new DeleteLessonSessionsCommand() { LessonSessionsId = lessonsSessionsId });
      return Json(new
      {
        result = response,
      });
    }


  }
}
