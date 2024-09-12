using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Enum
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum Member_LessonSessionsPaymentStatus
  {
    Unpaid = 0,
    Deposits = 1,
    FullyPaid = 2
  }
}
