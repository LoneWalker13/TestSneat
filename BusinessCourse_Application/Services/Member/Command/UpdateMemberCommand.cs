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
  public class UpdateMemberCommand : IRequest<Result>
  {
    public int Id { get; set; }
    public string ChineseName { get; set; }
    public string? EnglishName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? MemberCode { get; set; }
    public string? Remark { get; set; }
    public int  MembershipId { get; set; }
    public int Status { get; set; }

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateMemberCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
      {

        var member = _context.Members.FirstOrDefault(x => x.Id == request.Id);
        _mapper.Map(request, member);
        _context.Members.Update(member);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());
      }
    }
  }
}
