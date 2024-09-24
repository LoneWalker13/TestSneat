using BusinessCourse_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Interfaces
{
  public interface IMembership
  {
    Task<List<Membership>> GetMembershipList();

    Task<Membership> GetMembershipById(int membershipId);
  }
}
