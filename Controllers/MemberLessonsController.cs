using Amazon.Runtime.Internal;
using AutoMapper;
using BusinessCourse.Models.MemberLessons.MemberLessons;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.LessonSessions.Command;
using BusinessCourse_Application.Services.LessonSessions.Query;
using BusinessCourse_Application.Services.MemberLessons.Command;
using BusinessCourse_Application.Services.MemberLessons.Query;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BusinessCourse.Controllers
{
  public class MemberLessonsController : MediatRController
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public MemberLessonsController(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
      var model = new MemberLessonsViewModel();
      var lessons = await Mediator.Send(new GetLessonsQuery());
      List<SelectListItem> lessonsItems = new List<SelectListItem>() { new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true } };
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
      model.LessonsList = lessonsItems;

      return View("MemberLessons/Index",model);
    }


    [HttpGet]
    public async Task<List<LessonSessions>> GetLessonSessionsByLessonsId (int lessonsId)
    {
      var lessonsSessions = await Mediator.Send(new GetLessonsSessionListByLessonsIdQuery() { LessonId = lessonsId });

      return lessonsSessions;
    }


    [HttpGet]
    public async Task<IActionResult> GetMemberLessonsList(int LessonSessionId, string name, Member_LessonSessionsPaymentStatus paymentStatus, Member_LessonSessionsAttendanceStatus attendAttendanceStatus)
    {
      var memberLessons = await Mediator.Send(new GetMemberLessonsQuery()
      {
        LessonSessionId = LessonSessionId,
        Name = name,
        PaymentStatus = paymentStatus,
        AttendanceStatus = attendAttendanceStatus
      });

      var JsonResult = JsonConvert.SerializeObject(new { aaData = memberLessons });

      return Json(JsonResult);
    }


    [HttpGet]
    public async Task<IActionResult> SignUpMemberLessons()
    {
      var model = new SignUpMemberLessonsCommand();
      var lessons = await Mediator.Send(new GetLessonsQuery());
      List<SelectListItem> lessonsItems = new List<SelectListItem>() { new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true } };
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
      ViewBag.LessonsList = lessonsItems;
      return PartialView("MemberLessons/_MemberLessonsAdd", model);
    }


    [HttpPost]
    public async Task<IActionResult> SignUpMemberLessons(SignUpMemberLessonsCommand request)
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        var lessons = await Mediator.Send(new GetLessonsQuery());
        List<SelectListItem> lessonsItems = new List<SelectListItem>() { new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true } };
        lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
        ViewBag.LessonsList = lessonsItems;
        return PartialView("MemberLessons/_MemberLessonsAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }

    [HttpGet]
    public async Task<IActionResult> SignUpListMemberLessons()
    {
      var model = new SignUpListMemberLessonsCommand();
      var lessons = await Mediator.Send(new GetLessonsQuery());
      List<SelectListItem> lessonsItems = new List<SelectListItem>() { new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true } };
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
      ViewBag.LessonsList = lessonsItems;
      return PartialView("MemberLessons/_MemberLessonsBatchAdd", model);
    }

    [HttpGet]
    public async Task<IActionResult> SignUpListMemberLessons(SignUpListMemberLessonsCommand request )
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        var lessons = await Mediator.Send(new GetLessonsQuery());
        List<SelectListItem> lessonsItems = new List<SelectListItem>() { new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true } };
        lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));
        ViewBag.LessonsList = lessonsItems;
        return PartialView("MemberLessons/_MemberLessonsBatchAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }

    [HttpGet]
    public async Task<IActionResult> EditMemberLessonSessions(int memberLessonSessionsId)
    {
      var memberLessonSession = await Mediator.Send(new GetMemberLessonsByIdQuery() { MemberLessonsId = memberLessonSessionsId });
      var model = _mapper.Map(memberLessonSession, new UpdateMemberLessonsCommand());
      return PartialView("MemberLessons/_MemberLessonsEdit", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditMemberLessonSessions(UpdateMemberLessonsCommand command)
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        return PartialView("MemberLessons/_MemberLessonsEdit", command);
      }
      var response = await Mediator.Send(command);
      return Json(new
      {
        result = response,
      });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMemberLessonSessions(int memberLessonSessionsId)
    {
      var response = await Mediator.Send(new DeleteMemberLessonsCommand() { MemberLessonsSessionId = memberLessonSessionsId});
      return Json(new
      {
        result = response,
      });
    }

    [HttpPost]
    public async Task<JsonResult> UpdateAttendance(int memberLessonId, Member_LessonSessionsAttendanceStatus status)
    {
      var response = await Mediator.Send(new UpdateMemberLessonsAttendanceCommand { MemberLessonsId = memberLessonId, AttendanceStatus = status });
      return Json(new
      {
        result = response,
      });
    }
  }
}
