using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.LessonSessions.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Membership.Query
{
  public class GetMembershipListQuery : IRequest <List<BusinessCourse_Core.Entities.Membership>>
  {
  }

  public class GetMembershipListQueryHandler : IRequestHandler<GetMembershipListQuery, List<BusinessCourse_Core.Entities.Membership>>
  {
    private readonly IMembership _membership;

    public GetMembershipListQueryHandler(IMembership membership)
    {
      _membership = membership;
    }

    public async Task<List<BusinessCourse_Core.Entities.Membership>> Handle(GetMembershipListQuery request, CancellationToken cancellationToken)
    {
      var memberships = await _membership.GetMembershipList();

      return memberships;
    }
  }
}
