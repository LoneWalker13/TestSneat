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
    未付款 = 0,
    提交定金 = 1,
    全额付款 = 2,
    全部 = 3
  }
}
