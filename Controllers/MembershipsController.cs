using AutoMapper;
using BusinessCourse_Application.Services.Lessons.Command;
using BusinessCourse_Application.Services.Lessons.Query;
using BusinessCourse_Application.Services.Membership.Command;
using BusinessCourse_Application.Services.Membership.Query;
using BusinessCourse_Core.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BusinessCourse.Controllers
{
  public class MembershipsController : MediatRController
  {
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    //private readonly IDateTimeService _dateTimeService;
    public MembershipsController(IConfiguration configuration, IMapper mapper)
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
      return View("Memberships/Index");
    }

    public async Task<IActionResult> GetMembershipsList()
    {
      var memberships = await Mediator.Send(new GetLessonsQuery());

      var JsonResult = JsonConvert.SerializeObject(new { aaData = memberships });

      return Json(JsonResult);
    }


    [HttpGet]
    public async Task<IActionResult> AddMemberships()
    {

      var model = new AddMembershipCommand();
      return PartialView("Memberships/_MembershipsAdd", model);
    }

    [HttpPost]
    public async Task<IActionResult> AddMemberships(AddMembershipCommand request)
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        return PartialView("Memberships/_MembershipsAdd", request);
      }

      var response = await Mediator.Send(request);
      return Json(new
      {
        result = response,
      });
    }

    [HttpGet]
    public async Task<IActionResult> EditMemberships(int membershipId)
    {

      var membership = await Mediator.Send(new GetMembershipByIdQuery() { MembershipId = membershipId });

      var model = _mapper.Map(membership, new UpdateMembershipCommand());
      return PartialView("Memberships/_MembershipsEdit", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditMemberships(UpdateMembershipCommand command)
    {
      if (!ModelState.IsValid)
      {
        Response.StatusCode = 400;
        return PartialView("Memberships/_MembershipsEdit", command);
      }
      var response = await Mediator.Send(command);
      return Json(new
      {
        result = response,
      });
    }
  }
}
