using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Enum
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum LessonsStatus
  {
    Inactive = 0,
    Active = 1,
    All = 2,
  }
}
