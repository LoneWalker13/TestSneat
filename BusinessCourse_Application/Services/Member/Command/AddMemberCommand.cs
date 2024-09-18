using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Member.Command
{
  public class AddMemberCommand : IRequest<Result>
  {
    public string ChineseName { get; set; }
    public string EnglishName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string MemberCode { get; set; }

    public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public AddMemberCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(AddMemberCommand request, CancellationToken cancellationToken)
      {
        
        var existMember = _context.Members.FirstOrDefault(x=>x.PhoneNumber.Equals(request.PhoneNumber.Trim()));
        if (existMember == null) {
          var member = _mapper.Map<Members>(request);
          _context.Members.Add(member);
          await _context.SaveChangesAsync(cancellationToken);

          return new Result(true, new List<string>());
        }

        return new Result(false, new List<string>() { "MemberExist" });
      }
    }
  }
}
