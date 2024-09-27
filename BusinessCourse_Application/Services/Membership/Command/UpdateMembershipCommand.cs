using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Membership.Command
{
  public class UpdateMembershipCommand : IRequest<Result>
  {
    public int MembershipId { get; set; }
    public string Rank { get; set; }
    public int Tier { get; set; }

    public class UpdateMembershipCommandHandler : IRequestHandler<UpdateMembershipCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public UpdateMembershipCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(UpdateMembershipCommand request, CancellationToken cancellationToken)
      {
        var membership = _context.Membership.FirstOrDefault(x=>x.Id == request.MembershipId);
        _mapper.Map(request, membership);
        _context.Membership.Update(membership);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
