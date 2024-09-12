using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Exceptions
{
  public class BadRequestException : Exception
  {
    public BadRequestException() : base() { }

    public BadRequestException(string msg) : base(msg)
    {
    }
    public string detail { get; set; }
  }
}
