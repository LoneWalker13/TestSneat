using AutoMapper;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Core.Common;
using BusinessCourse_Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

      var statusSelectedList =Enum.GetValues(typeof(LessonsStatus))
               .Cast<LessonsStatus>()
               .Select(e => new SelectListItem
               {
                 Value = Convert.ToInt32(e).ToString(),
                 Text = e.ToString(),
                 Selected = e == LessonsStatus.All // Set the selected value
               }).ToList();
      ViewBag.LessonsStatus = statusSelectedList.OrderByDescending(x=>x.Value);
      return View("Lessons/Index");
    }

    public async Task<IActionResult>  GetLessonsList(string searchString,LessonsStatus status)
    {
      var lessons = await Mediator.Send(new GetLessonsQuery());
      if ((int)status != 2)
        lessons = lessons.Where(x => x.Status == status).ToList();

      if (!string.IsNullOrEmpty(searchString))
        lessons = lessons.Where(r => r.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)).ToList();


      lessons.ForEach(x => x.CreatedStr = DateTimeHelper.GetUtcDateTime(x.Created));

      var JsonResult = JsonConvert.SerializeObject(new { aaData = lessons });

      return Json(JsonResult);
    }


    [HttpGet]
    public async Task<IActionResult> AddLessons()
    {

      var model = new AddLessonCommand();
      return PartialView("Lessons/_LessonsAdd", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddLessons(AddLessonCommand request)
    {
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
    public async Task<IActionResult> EditLessons(int id)
    {
      var lesson = await Mediator.Send(new GetLessonsByIdQuery() {LessonsId = id });

      var model = _mapper.Map(lesson, new UpdateLessonsCommand());
      return PartialView("Lessons/_LessonsEdit", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditLessons(UpdateLessonsCommand command)
    {
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
