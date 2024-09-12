using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Services
{
  public class ErrorResponse
  {
    public string type { get; set; }
    public string title { get; set; }
    public HttpStatusCode status { get; set; }
    public string detail { get; set; }
  }
}
