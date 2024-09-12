using BusinessCourse_Application.Interfaces;
using BusinessCourse_Infrastructure.Persistence;
using BusinessCourse_Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure
{
  public static  class DependancyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

      services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer(
      configuration.GetConnectionString("DefaultConnection"),
      b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


      services.AddTransient<IDateTime, DateTimeService>();

      //services.AddDbContext<IntegrationEventLogContext>(options =>
      //{
      //  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
      //      sqlOptions =>
      //      {
      //        sqlOptions.MigrationsAssembly("Api.Integration.Infrastructure");
      //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
      //      });
      //});

      //services.AddTransient<IIdentityService, IdentityService>();
      //services.AddTransient<IDateTimeService, DateTimeService>();
      //var redis = ConnectionMultiplexer.Connect(configuration["ConnectionStrings:Redis"]);
      var appName = configuration["Program:AppName"];
      //services.AddDataProtection().SetApplicationName(appName)
      //    .PersistKeysToStackExchangeRedis(redis, $"DataProtection-Keys-{appName}");
      return services;
    }
  }
}
