using AutoMapper;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.LessonSessions.Command;
using BusinessCourse_Application.Services.LessonSessions.Query;
using BusinessCourse_Core.Common;
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

      var statusSelectedList = Enum.GetValues(typeof(LessonSessionsStatus))
               .Cast<LessonSessionsStatus>()
               .Select(e => new SelectListItem
               {
                 Value = Convert.ToInt32(e).ToString(),
                 Text = e.ToString(),
                 Selected = e == LessonSessionsStatus.All // Set the selected value
               }).ToList();
      ViewBag.LessonSessionsStatus = statusSelectedList.OrderByDescending(x => x.Value);
      var lessons = await GetActiveLessons();
      ViewBag.LessonsList = lessons;
      return View("LessonSessions/Index");
    }


    public async Task<IActionResult> GetLessonsSessionsByLessonId(int lessonsId, LessonSessionsStatus status)
    {
      var lessonsSessions = await Mediator.Send(new GetLessonsSessionListByLessonsIdQuery() { LessonId = lessonsId });
      if ((int)status != 2)
        lessonsSessions = lessonsSessions.Where(x => x.Status == (int)status).ToList();


      lessonsSessions.ForEach(x => x.SessionDateStr = DateTimeHelper.GetUtcDateTime(x.SessionDate));

      var JsonResult = JsonConvert.SerializeObject(new { aaData = lessonsSessions });

      return Json(JsonResult);
    }

    public async Task<List<SelectListItem>> GetActiveLessons()
    {
      var lessons = await Mediator.Send(new GetLessonsQuery());
      lessons = lessons.Where(x=>x.Status == LessonsStatus.Active).ToList();
      List<SelectListItem> lessonsItems = new List<SelectListItem>();
      lessonsItems.Add(new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true });
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
      return lessonsItems;
    }

    [HttpGet]
    public async Task<IActionResult> AddLessonSessions()
    {
      var lessons = await GetActiveLessons();
      ViewBag.LessonsList = lessons;
      var model = new AddLessonSessionsCommand();
      return PartialView("LessonSessions/_LessonSessionsAdd", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddLessonSessions(AddLessonSessionsCommand request)
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        var lessons = await GetActiveLessons();
        ViewBag.LessonsList = lessons;
        return PartialView("LessonSessions/_LessonSessionsAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }


    [HttpGet]
    public async Task<IActionResult> EditLessonSessions(int id)
    {
      var lessonSession = await Mediator.Send(new GetLessonSessionsByIdQuery() { LessonSessionsId = id });
      lessonSession.SessionDate = DateTimeHelper.ConvertToNormal(lessonSession.SessionDate);
      var lessons = await GetActiveLessons();
      ViewBag.LessonsList = lessons;
      var model = _mapper.Map(lessonSession, new UpdateLessonSessionsCommand());
      return PartialView("LessonSessions/_LessonSessionsEdit", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditLessonSessions(UpdateLessonSessionsCommand command)
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        var lessons = await GetActiveLessons();
        ViewBag.LessonsList = lessons;
        return PartialView("LessonSessions/_LessonSessionsEdit", command);
      }
      var response = await Mediator.Send(command);
      return Json(new
      {
        result = response,
      });
    }


    [HttpGet]
    public async Task<IActionResult> DeleteLessonSessions(int id)
    {
      var model = new DeleteLessonSessionsCommand() { Id = id };
      return PartialView("LessonSessions/_LessonSessionsDelete", model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteLessonSessions(DeleteLessonSessionsCommand command)
    {

      var response  = await Mediator.Send(command);
      return Json(new
      {
        result = response,
      });
    }


  }
}
