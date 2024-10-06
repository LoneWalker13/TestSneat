
using AutoMapper;
using AutoMapper.Internal;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Application.Services.Member.Query;
using BusinessCourse_Application.Services.Membership.Query;
using BusinessCourse_Core.Common;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using BusinessCourse_Infrastructure.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BusinessCourse.Controllers
{
  public class MembersController : MediatRController
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public MembersController(IConfiguration configuration, IMapper mapper)
    {
      _configuration = configuration;
      _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
      var membershipsItems = await GetMemberships();
      ViewBag.RankList = membershipsItems;
      return View("Members/Index");
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberList(DateTime dateFrom, DateTime dateTo, string name, string phoneNumber,  string memberCode, int rank)
    {
      var response = await Mediator.Send(new GetMemberListQuery() {Name = name, PhoneNumber = phoneNumber, MemberCode= memberCode,From = dateFrom, To = dateTo,Rank = rank });
      response.ForEach(x => x.CreatedStr = DateTimeHelper.GetUtcDateTime(x.Created));
      var JsonResult = JsonConvert.SerializeObject(new { aaData = response });

      return Json(JsonResult);
    }

    private async Task<List<SelectListItem>> GetMemberships()
    {
      var memberships = await Mediator.Send(new GetMembershipListQuery());
      memberships = memberships.Where(x => x.Status).ToList();
      List<SelectListItem> membershipsItems = new List<SelectListItem>();
      membershipsItems.Add(new SelectListItem() { Text = "---   全部   ---", Value = "0", Selected = true });
      memberships.ToList().ForEach(r => membershipsItems.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Rank }));
      return membershipsItems;
    }



    [HttpGet]
    public async Task<IActionResult> UpdateMember(int id)
    {
      var member = await Mediator.Send(new GetMemberByIdQuery() { MemberId = id });

      var membershipsItems = await GetMemberships();
      membershipsItems.RemoveAt(0);
      ViewBag.RankList = membershipsItems;

      var model = _mapper.Map(member, new UpdateMemberCommand());

      return PartialView("Members/_MembersEdit", model);
    }

    [HttpGet]
    public async Task<IActionResult> MemberLessonSession(string loginId, string currentTab = "MemberLessonSession")
    {
      //ViewBag.LoginId = loginId;
      ViewBag.currentTransactionTab = currentTab;
      var lessons = await Mediator.Send(new GetLessonsQuery());
      //ViewBag.IsMW = _currentTenantService.IsMW;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;
      return PartialView("Members/_MemberLessionSession", lessons);
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberLessonSessionsQuery(int memberId, int lessonId)
    {
      var response = await Mediator.Send(new GetMemberLessonSessionsQuery() {MemberId = memberId, LessonId = lessonId});
      var JsonResult = JsonConvert.SerializeObject(new { aaData = response });

      return Json(JsonResult);
    }


    [HttpGet]
    public async Task<IActionResult> AddMember()
    {
      var membershipsItems = await GetMemberships();
      membershipsItems.RemoveAt(0);
      ViewBag.RankList = membershipsItems;
      return PartialView("Members/_MembersAdd");
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(AddMemberCommand request )
    {
      if (!ModelState.IsValid)
      {
        var membershipsItems = await GetMemberships();
        membershipsItems.RemoveAt(0);
        ViewBag.RankList = membershipsItems;
        Response.StatusCode = 400;
        return PartialView("Members/_MembersAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMember(UpdateMemberCommand request)
    {
      if (!ModelState.IsValid)
      {
        var membershipsItems = await GetMemberships();
        membershipsItems.RemoveAt(0);
        ViewBag.RankList = membershipsItems;
        Response.StatusCode = 400;
        return PartialView("Members/_MembersEdit", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }

  }
}
