using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Member.Query
{
  public class GetMemberByIdQuery : IRequest<Members>
  {
    public int MemberId { get; set; }
  }

  public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Members>
  {
    private readonly IMember _member;

    public GetMemberByIdQueryHandler(IMember member)
    {
      _member = member;
    }

    public async Task<Members> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
      var member = await _member.GetMemberById(request.MemberId);

      return member;
    }
  }
}
