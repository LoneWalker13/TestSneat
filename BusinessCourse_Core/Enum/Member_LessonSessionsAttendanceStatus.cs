using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Enum
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum Member_LessonSessionsAttendanceStatus
  {
    未出席 = 0,
    出席 = 1,
    全部 = 2,
  }
}
