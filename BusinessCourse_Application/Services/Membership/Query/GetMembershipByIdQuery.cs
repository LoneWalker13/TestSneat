using BusinessCourse_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Membership.Query
{
  public class GetMembershipByIdQuery : IRequest<BusinessCourse_Core.Entities.Membership>
  {
    public int MembershipId { get; set; }
  }

  public class GetMembershipByIdQueryHandler : IRequestHandler<GetMembershipByIdQuery, BusinessCourse_Core.Entities.Membership>
  {
    private readonly IMembership _membership;

    public GetMembershipByIdQueryHandler(IMembership membership)
    {
      _membership = membership;
    }

    public async Task<BusinessCourse_Core.Entities.Membership> Handle(GetMembershipByIdQuery request, CancellationToken cancellationToken)
    {
      var membership = await _membership.GetMembershipById(request.MembershipId);

      return membership;
    }
  }
}
