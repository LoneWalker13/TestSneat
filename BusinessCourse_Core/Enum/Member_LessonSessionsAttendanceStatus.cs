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
    Unattend = 0,
    Attended = 1
  }
}
