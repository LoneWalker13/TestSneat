using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Exceptions
{
  public class ExceptionResponse
  {
    [JsonPropertyName("error")]
    public string error { get; set; }
  }
}
