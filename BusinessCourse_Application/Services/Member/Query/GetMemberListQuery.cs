using AutoMapper;
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
  public class GetMemberListQuery : IRequest<List<Members>>
  {
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string MemberCode { get; set; }

  }

  public class GetMemberListQueryHandler : IRequestHandler<GetMemberListQuery, List<Members>>
  {
    private readonly IMember _member;

    public GetMemberListQueryHandler(IMember member)
    {
      _member = member;
    }

    public async Task<List<Members>> Handle(GetMemberListQuery request, CancellationToken cancellationToken)
    {
      var members = await _member.GetMemberList(request.Name,request.PhoneNumber,request.MemberCode);

      return members;
    }
  }
}
