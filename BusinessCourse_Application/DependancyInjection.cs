using AutoMapper;
using BusinessCourse_Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace BusinessCourse_Application
{
  public static class DependancyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
      AddMediatR(services);
      AddAutoMapper(services);
      AddFluentValidation(services);
      AddOptions(services, configuration);

      //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
      ////services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
      //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
      services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

      return services;
    }

    public static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
      //services.Configure<ProgramOptions>(configuration.GetSection(ProgramOptions.Program));
      //services.Configure<GraylogOptions>(configuration.GetSection(GraylogOptions.Graylog));
    }
    public static void AddMediatR(IServiceCollection services)
    {
      services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    public static void AddAutoMapper(IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void AddFluentValidation(IServiceCollection services)
    {
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

  }
}
