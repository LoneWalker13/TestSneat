using MediatR;
using Microsoft.AspNetCore.Localization;
using System.Text.RegularExpressions;

namespace BusinessCourse.Common.Cultures
{
  public class CurrentCultureService 
  {
    //private readonly IMediator _mediator;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    //private readonly ILanguageService _languageService;
    //public CurrentCultureService(IMediator mediator, IHttpContextAccessor httpContextAccessor, ILanguageService languageService)
    //{
    //  _mediator = mediator;
    //  _httpContextAccessor = httpContextAccessor;
    //  _languageService = languageService;
    //}

    //public async Task<int> GetLanguageId()
    //{
    //  var languageId = 0;
    //  var currentCulture = _httpContextAccessor.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

    //  if (!string.IsNullOrEmpty(currentCulture))
    //  {
    //    Match match = Regex.Match(currentCulture, @"^c=([a-zA-Z\-]+)\|\w+",
    //    RegexOptions.IgnoreCase);
    //    //languageId = match.Success ? ConstantField.Language.LanguageID(match.Groups[1].Value) : 0;

    //  }
    //  if (languageId.Equals(0))
    //  {
    //    currentCulture = _httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name;
    //    if (currentCulture.Contains("zh"))
    //    {
    //      currentCulture = "zh";
    //    }
    //    else if (currentCulture.Contains("ms"))
    //    {
    //      currentCulture = "ms";
    //    }
    //    else if (currentCulture.Contains("en"))
    //    {
    //      currentCulture = "en";
    //    }
    //    else
    //    {
    //      currentCulture = "en";
    //    }

    //    var languageList = await _languageService.GetLanguageList();

    //    var language = languageList.FirstOrDefault(lang => lang.Name == currentCulture);

    //    foreach (var lang in languageList)
    //    {
    //      if (lang.LanguageCountryCode.Contains(currentCulture))
    //      {
    //        languageId = lang.Id;
    //      }
    //    }

    //    _httpContextAccessor.HttpContext.Response.Cookies.Append(
    //        CookieRequestCultureProvider.DefaultCookieName,
    //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(currentCulture)),
    //        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    //    );
    //  }
    //  return languageId;
    //}

    //public string GetLanguageStr()
    //{
    //  var languagestr = "en";
    //  var currentCulture = _httpContextAccessor.HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

    //  if (!string.IsNullOrEmpty(currentCulture))
    //  {
    //    Match match = Regex.Match(currentCulture, @"^c=([a-zA-Z\-]+)\|\w+",
    //    RegexOptions.IgnoreCase);
    //    languagestr = match.Success ? match.Groups[1].Value : "en";
    //  }
    //  return languagestr;
    //}
  }
}
