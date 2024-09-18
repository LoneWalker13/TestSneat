using Autofac.Core;
using BusinessCourse.Common.Filter;
using BusinessCourse_Application;
using BusinessCourse_Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using AutoMapper;

namespace BusinessCourse
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;

      Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddInfrastructure(Configuration);

      services.AddApplication(Configuration);

      //services.AddSingleton<ICurrentUserService, CurrentUserService>();
      //services.AddSingleton<ICurrentTenantService, CurrentTenantService>();

      services.AddHttpContextAccessor();


      services.AddControllersWithViews(options =>
      {
        options.Filters.Add<ApiExceptionFilterAttribute>();
      }).AddFluentValidation(fv =>
      {
        fv.ImplicitlyValidateChildProperties = true;
      });
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
      services.AddAuthentication(options =>
      {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
      });
      //  .AddCookie("Cookies", options => {

      //  options.SessionStore = new RedisCacheTicketStore(new RedisCacheOptions()
      //  {
      //    Configuration = Configuration.GetConnectionString("Redis"),
      //    InstanceName = Configuration["Program:AppName"]

      //  });
      //  options.Events = new CustomCookieAuthenticationEvents(Configuration, Environment);
      //})

      services.AddLocalization(options => options.ResourcesPath = "Resources");
      services.AddMvc()
         .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
         .AddDataAnnotationsLocalization()
         .AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));


      services.Configure<RequestLocalizationOptions>(options =>
      {
        var cultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("zh"),
                    new CultureInfo("ms")
                };
        options.DefaultRequestCulture = new RequestCulture("en");
        options.SupportedCultures = cultures;
        options.SupportedUICultures = cultures;
        options.SetDefaultCulture("en");
      });
      services.AddRazorPages().AddRazorRuntimeCompilation();
      services.AddHealthChecks();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      var fordwardedHeaderOptions = new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      };
      fordwardedHeaderOptions.KnownNetworks.Clear();
      fordwardedHeaderOptions.KnownProxies.Clear();

      app.UseForwardedHeaders(fordwardedHeaderOptions);

      app.UseCookiePolicy();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        IdentityModelEventSource.ShowPII = true;
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHealthChecks("/health");

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

      app.UseHttpsRedirection();
      app.UseStaticFiles();


      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapHealthChecks("/health", new HealthCheckOptions()
        {
          ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
          AllowCachingResponses = false
        });
      });

    }
  }
}
