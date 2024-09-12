using BusinessCourse_Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessCourse_Application.Exceptions
{
  public static class EnsureSuccessStatusHandling
  {
    public static async Task EnsureSuccessStatusHandle(this HttpResponseMessage httpResponse)
    {
      if (!httpResponse.IsSuccessStatusCode)
      {
        using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
        var errorResponse = await JsonSerializer.DeserializeAsync<ErrorResponse>(responseStream);
        var errorMessage = errorResponse?.title ?? "Unknown error occurred.";
        throw new Exception(errorMessage);
      }
    }
  }
}
