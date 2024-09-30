using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Repository
{
  public class MembershipRepository : EFRepsitory<Membership>, IMembership
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public MembershipRepository(IConfiguration configuration, ApplicationDbContext context) : base(context)
    {
      _configuration = configuration;
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Membership>> GetMembershipList()
    {
      var entity = await _context.Membership
      .OrderBy(x => x.Tier)
      .ToListAsync();

      return entity;

    }

    public async Task<Membership> GetMembershipById(int membershipId)
    {
      var entity = await _context.Membership
      .FirstOrDefaultAsync(x => x.Id == membershipId);

      return entity;
    }
  }
}
