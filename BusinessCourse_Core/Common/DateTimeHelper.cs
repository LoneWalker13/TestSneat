using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Core.Common
{
  public static class DateTimeHelper
  {
    public static int Hours = 8;

    public static DateTime ConvertToNormal(DateTime datetime)
    {
      return datetime.AddHours(Hours);
    }

    public static string GetUtcDateTime(DateTime datetime)
    {
      return datetime.AddHours(Hours).ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static DateTime ConvertToUtc(DateTime datetime)
    {
      return datetime.AddHours(-Hours);
    }
  }
}
