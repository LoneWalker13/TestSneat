using BusinessCourse_Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Services
{
  public class DateTimeService : IDateTime
  {
    public DateTime Now => DateTime.UtcNow;
  }
}
