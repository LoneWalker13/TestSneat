using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Exceptions
{
  public class ConflictException : Exception
  {
    public ConflictException() : base() { }

    public ConflictException(string msg) : base(msg)
    {

    }
  }
}
