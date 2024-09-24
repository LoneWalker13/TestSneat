using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCourse.Controllers
{
  public abstract class MediatRController : Controller
  {
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    //protected int _timezone => 8;
  }
}
