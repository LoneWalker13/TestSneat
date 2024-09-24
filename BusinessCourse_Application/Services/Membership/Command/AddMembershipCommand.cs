using AutoMapper;
using BusinessCourse_Application.Common.Model;
using BusinessCourse_Application.Interfaces;
using BusinessCourse_Application.Services.Member.Command;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services.Membership.Command
{
  public class AddMembershipCommand : IRequest<Result>
  {
    public string Rank { get; set; }
    public string Tier { get; set; }

    public class AddMembershipCommandHandler : IRequestHandler<AddMembershipCommand, Result>
    {
      private readonly IApplicationDbContext _context;
      private readonly IMapper _mapper;

      public AddMembershipCommandHandler(IApplicationDbContext context, IMapper mapper)
      {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      }

      public async Task<Result> Handle(AddMembershipCommand request, CancellationToken cancellationToken)
      {

        var membership = _mapper.Map<BusinessCourse_Core.Entities.Membership>(request);
        _context.Membership.Add(membership);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result(true, new List<string>());

      }
    }
  }
}
