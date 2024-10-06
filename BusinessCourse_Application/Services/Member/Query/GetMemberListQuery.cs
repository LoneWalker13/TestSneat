using AutoMapper;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Common;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Member.Query
{
  public class GetMemberListQuery : IRequest<List<ViewMemberList>>
  {
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string MemberCode { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int Rank { get; set; }

  }

  public class GetMemberListQueryHandler : IRequestHandler<GetMemberListQuery, List<ViewMemberList>>
  {
    private readonly IMember _member;

    public GetMemberListQueryHandler(IMember member)
    {
      _member = member;
    }

    public async Task<List<ViewMemberList>> Handle(GetMemberListQuery request, CancellationToken cancellationToken)
    {
      request.From = DateTimeHelper.ConvertToUtc(request.From.Date);
      request.To = DateTimeHelper.ConvertToUtc(request.To.Date.AddDays(1));
      var members = await _member.GetMemberList(request.Name,request.PhoneNumber,request.MemberCode,request.From,request.To,request.Rank);

      return members;
    }
  }
}
