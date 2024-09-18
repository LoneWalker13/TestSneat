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
  public class GetMemberByPhoneNumberQuery : IRequest<Members>
  {
    public string PhoneNumber { get; set; }
  }

  public class GetMemberByPhoneNumberQueryHandler : IRequestHandler<GetMemberByPhoneNumberQuery, Members>
  {
    private readonly IMember _member;

    public GetMemberByPhoneNumberQueryHandler(IMember member)
    {
      _member = member;
    }

    public async Task<Members> Handle(GetMemberByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
      var member = await _member.GetMemberByPhoneNumber(request.PhoneNumber);

      return member;
    }
  }
}
