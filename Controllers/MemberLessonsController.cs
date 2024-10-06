using Amazon.Runtime.Internal;
using AutoMapper;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.LessonSessions.Command;
using BusinessCourse_Application.Services.LessonSessions.Query;
using BusinessCourse_Application.Services.MemberLessons.Command;
using BusinessCourse_Application.Services.MemberLessons.Query;
using BusinessCourse_Core.Common;
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
      var attendStatusSelectedList = Enum.GetValues(typeof(Member_LessonSessionsAttendanceStatus))
         .Cast<Member_LessonSessionsAttendanceStatus>()
         .Select(e => new SelectListItem
         {
           Value = Convert.ToInt32(e).ToString(),
           Text = e.ToString(),
           Selected = e == Member_LessonSessionsAttendanceStatus.全部 // Set the selected value
         }).ToList();
      ViewBag.AttendStatus = attendStatusSelectedList.OrderByDescending(x => x.Value);

      var paymentStatusSelectedList = Enum.GetValues(typeof(Member_LessonSessionsPaymentStatus))
         .Cast<Member_LessonSessionsPaymentStatus>()
         .Select(e => new SelectListItem
         {
           Value = Convert.ToInt32(e).ToString(),
           Text = e.ToString(),
           Selected = e == Member_LessonSessionsPaymentStatus.全部 // Set the selected value
         }).ToList();
      ViewBag.PaymentStatus = paymentStatusSelectedList.OrderByDescending(x => x.Value);


      var lessons = await Mediator.Send(new GetLessonsQuery());
      List<SelectListItem> lessonsItems = new List<SelectListItem>() { new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true } };
      lessons.ToList().ForEach(r => lessonsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Name }));

      ViewBag.LessonList = lessonsItems;
      return View("MemberLessons/Index");
    }


    [HttpGet]
    public async Task<List<LessonSessions>> GetLessonSessionsByLessonsId (int id)
    {
      var lessonsSessions = await Mediator.Send(new GetLessonsSessionListByLessonsIdQuery() { LessonId = id });
      lessonsSessions.ForEach(x => x.SessionDateStr = DateTimeHelper.GetUtcDateTime(x.SessionDate));

      return lessonsSessions;
    }
    [HttpGet]
    public async Task<List<LessonSessions>> GetAddLessonSessionsByLessonsId(int id)
    {
      var lessonsSessions = await Mediator.Send(new GetLessonsSessionListByLessonsIdQuery() { LessonId = id });
      lessonsSessions.ForEach(x => x.SessionDateStr = DateTimeHelper.GetUtcDateTime(x.SessionDate));
      lessonsSessions = lessonsSessions.Where(x => x.Status == 1).ToList();

      return lessonsSessions;
    }


    [HttpGet]
    public async Task<IActionResult> GetMemberLessonsList(int lessonsId,int lessonSessionsId, string name , string phoneNumber, Member_LessonSessionsPaymentStatus paymentStatus, Member_LessonSessionsAttendanceStatus attendStatus)
    {
      var memberLessons = new List<GetMemberLessonSessionsList>();
      if (lessonsId > 0 && lessonSessionsId > 0)
      {
        memberLessons = await Mediator.Send(new GetMemberLessonsQuery()
        {
          LessonId = lessonsId,
          PhoneNumber = phoneNumber,
          LessonSessionsId = lessonSessionsId,
          PaymentStatus = paymentStatus,
          Name = name,
          AttendanceStatus = attendStatus
        });
        memberLessons.ForEach(x => x.CreatedStr = DateTimeHelper.GetUtcDateTime(x.Created));
      }
      var JsonResult = JsonConvert.SerializeObject(new { aaData = memberLessons });

      return Json(JsonResult);
    }


    [HttpGet]
    public async Task<IActionResult> SignUpMemberLessons()
    {
      var paymentStatusSelectedList = Enum.GetValues(typeof(Member_LessonSessionsPaymentStatus))
     .Cast<Member_LessonSessionsPaymentStatus>()
     .Select(e => new SelectListItem
     {
       Value = Convert.ToInt32(e).ToString(),
       Text = e.ToString(),
       Selected = e == Member_LessonSessionsPaymentStatus.全部 // Set the selected value
     }).ToList();

      //paymentStatusSelectedList.OrderByDescending(x => x.Value).ToList();
      paymentStatusSelectedList = paymentStatusSelectedList.Where(x => x.Text != "全部").ToList();
      ViewBag.PaymentStatus = paymentStatusSelectedList;

      var attendStatusSelectedList = Enum.GetValues(typeof(Member_LessonSessionsAttendanceStatus))
       .Cast<Member_LessonSessionsAttendanceStatus>()
       .Select(e => new SelectListItem
       {
         Value = Convert.ToInt32(e).ToString(),
         Text = e.ToString(),
         Selected = e == Member_LessonSessionsAttendanceStatus.未出席 // Set the selected value
       }).ToList();
      attendStatusSelectedList = attendStatusSelectedList.Where(x => x.Text != "全部").ToList();
      ViewBag.AttendStatus = attendStatusSelectedList;

      var model = new SignUpMemberLessonsCommand();
      var lessons = await Mediator.Send(new GetLessonsQuery());
      lessons = lessons.Where(x => x.Status == LessonsStatus.Active).ToList();
      List<SelectListItem> lessonsItems = new List<SelectListItem>();
      lessons.OrderByDescending(x=>x.Id);
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
        var paymentStatusSelectedList = Enum.GetValues(typeof(Member_LessonSessionsPaymentStatus))
       .Cast<Member_LessonSessionsPaymentStatus>()
       .Select(e => new SelectListItem
       {
         Value = Convert.ToInt32(e).ToString(),
         Text = e.ToString(),
         Selected = e == Member_LessonSessionsPaymentStatus.全部 // Set the selected value
       }).ToList();

        //paymentStatusSelectedList.OrderByDescending(x => x.Value).ToList();
        paymentStatusSelectedList = paymentStatusSelectedList.Where(x => x.Text != "全部").ToList();
        ViewBag.PaymentStatus = paymentStatusSelectedList;

        var attendStatusSelectedList = Enum.GetValues(typeof(Member_LessonSessionsAttendanceStatus))
         .Cast<Member_LessonSessionsAttendanceStatus>()
         .Select(e => new SelectListItem
         {
           Value = Convert.ToInt32(e).ToString(),
           Text = e.ToString(),
           Selected = e == Member_LessonSessionsAttendanceStatus.全部 // Set the selected value
         }).ToList();
        attendStatusSelectedList = attendStatusSelectedList.Where(x => x.Text != "全部").ToList();
        ViewBag.AttendStatus = attendStatusSelectedList.OrderByDescending(x => x.Value);

        var model = new SignUpMemberLessonsCommand();
        var lessons = await Mediator.Send(new GetLessonsQuery());
        lessons = lessons.Where(x => x.Status == LessonsStatus.Active).ToList();
        List<SelectListItem> lessonsItems = new List<SelectListItem>();
        lessons.OrderByDescending(x => x.Id);
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
      lessons = lessons.Where(x=>x.Status == LessonsStatus.Active).ToList();
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
    public async Task<IActionResult> EditMemberLessonSessions(int id)
    {
      var memberLessonSession = await Mediator.Send(new GetMemberLessonsByIdQuery() { MemberLessonsId = id });
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
