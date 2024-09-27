
using AutoMapper.Internal;
using BusinessCourse.Models.Members.Members;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Application.Services.Member.Query;
using BusinessCourse_Application.Services.Membership.Query;
using BusinessCourse_Core.Entities;
using BusinessCourse_Infrastructure.Services;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BusinessCourse.Controllers
{
  public class MembersController : MediatRController
  {
    private readonly IConfiguration _configuration;
    //private readonly IDateTimeService _dateTimeService;
    public MembersController(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<IActionResult> MemberList()
    {

      return View("MemberList/MemberList");
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberList(string name, string phoneNumber,  string memberCode)
    {
      var response = await Mediator.Send(new GetMemberListQuery() {Name = name, PhoneNumber = phoneNumber, MemberCode= memberCode });
      var JsonResult = JsonConvert.SerializeObject(new { aaData = response });

      return Json(JsonResult);
    }


    //[CustomAuthorize(Roles = "member.view")]
    [HttpGet]
    public async Task<IActionResult> MemberDetails(string loginId, string currentTab = "AccountDetails")
    {
      //var breadcrumbs = new List<BreadcrumbItem>
      //      {
      //          new BreadcrumbItem { Title = "Home", Url = Url.Action("Index", "Home"), IsActive = false },
      //          new BreadcrumbItem { Title = "Member List", Url = Url.Action("MemberList", "Member"), IsActive = false },
      //          new BreadcrumbItem { Title = $"Member", Url = "#", IsActive = true },
      //      };
      //ViewBag.Breadcrumbs = breadcrumbs;
      //ViewBag.LoginId = loginId;
      //ViewBag.CurrentTab = currentTab;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;
      //ViewBag.IsMW = _currentTenantService.IsMW;
      return View("MemberList/MemberDetails");
    }


    [HttpGet]
    public async Task<IActionResult> UpdateMember(int memberId)
    {
      var member = await Mediator.Send(new GetMemberByIdQuery() { MemberId = memberId });

      var membershipList = await Mediator.Send(new GetMembershipListQuery());
      List<SelectListItem> membershipListItem = new List<SelectListItem>();
      membershipListItem.Add(new SelectListItem() { Text = "--- 请选择 ---", Value = "0", Selected = true });
      membershipList.ToList().ForEach(r => membershipListItem.Add(new SelectListItem() { Value = r.Id.ToString(), Text = r.Rank }));

      var model = new MemberViewModel() { Member = member,MembershipListItem = membershipListItem };

      return PartialView("MemberList/MemberEdit", model);
    }

    [HttpGet]
    public async Task<IActionResult> MemberLessonSession(string loginId, string currentTab = "MemberLessonSession")
    {
      //ViewBag.LoginId = loginId;
      ViewBag.currentTransactionTab = currentTab;
      var lessons = await Mediator.Send(new GetLessonsQuery());
      var model = new MemberLessionSessionModel() { Lessons = lessons };
      //ViewBag.IsMW = _currentTenantService.IsMW;
      //var clientTenants = await Mediator.Send(new GetClientUnderTenantByTenantIdQuery());
      //var clientTenant = clientTenants.Where(r => r.ClientId == _configuration["IdentityServer:ClientId"]).FirstOrDefault();
      //ViewBag.WalletType = clientTenant.WalletType;
      return PartialView("MemberList/_MemberLessionSession", lessons);
    }

    [HttpGet]
    public async Task<IActionResult> GetMemberLessonSessionsQuery(int memberId, int lessonId)
    {
      var response = await Mediator.Send(new GetMemberLessonSessionsQuery() {MemberId = memberId, LessonId = lessonId});
      var JsonResult = JsonConvert.SerializeObject(new { aaData = response });

      return Json(JsonResult);
    }


    [HttpGet]
    public IActionResult AddMember()
    {
      return PartialView("MemberList/_MemberAdd");
    }

    [HttpPost]
    public async Task<IActionResult> AddMember(string chineseName, string englishName, string phoneNumber, string email, string memberCode )
    {
      var response = await Mediator.Send(new AddMemberCommand()
      {
        ChineseName = chineseName,
        EnglishName = englishName,
        PhoneNumber = phoneNumber,
        Email = email,
        MemberCode = memberCode
      });
      return Json(new
      {
        result = response,
      });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMember(int memberId, string chineseName, string englishName, string email, string memberCode, string remark, int membershipId)
    {
      var response = await Mediator.Send(new UpdateMemberCommand()
      {
        MemberId = memberId,
        ChineseName = chineseName,
        EnglishName = englishName,
        Remark = remark,
        Email = email,
        MemberCode = memberCode,
        MembershipId = membershipId
      });
      return Json(new
      {
        result = response,
      });
    }

  }
}
