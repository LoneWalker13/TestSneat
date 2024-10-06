using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core;
using BusinessCourse_Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence
{
  public class ApplicationDbContext : DbContext, IApplicationDbContext
  {
    private readonly IMediator _mediator;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
    //ICurrentUserService currentUserService,
    //IDomainEventService domainEventService,
    IDateTime dateTime,
    IMediator mediator) : base(options)
    {
      _dateTime = dateTime;
      _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

      System.Diagnostics.Debug.WriteLine("ApplicationDbContext::ctor ->" + GetHashCode());
    }

    public DbSet<Members> Members { get; set; }
    public DbSet<Lessons> Lessons { get; set; }
    public DbSet<LessonSessions> LessonSessions { get; set; }
    public DbSet<MemberLessonSessions> MemberLessonSessions { get; set; }
    public DbSet<Membership> Membership { get; set; }
    public DbSet<ViewMemberList> ViewMemberList { get; set; }
    public DbSet<GetMemberLessonSessionsList> GetMemberLessonSessionsList { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
      {
        switch (entry.State)
        {
          case EntityState.Added:
            //entry.Entity.CreatedBy = _currentUserService.UserId;
            entry.Entity.CreatedBy = "system";
            entry.Entity.Created = _dateTime.Now;
            break;

          case EntityState.Modified:
            entry.Entity.LastModifiedBy = "system";
            //entry.Entity.LastModifiedBy = _currentUserService.UserId;
            entry.Entity.LastModified = _dateTime.Now;
            break;
        }
      }

      var result = await base.SaveChangesAsync(cancellationToken);

      //await DispatchEvents();

      return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      base.OnModelCreating(builder);
    }

    //private async Task DispatchEvents()
    //{
    //  while (true)
    //  {
    //    var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
    //        .Select(x => x.Entity.DomainEvents)
    //        .SelectMany(x => x)
    //        .Where(domainEvent => !domainEvent.IsPublished)
    //        .FirstOrDefault();
    //    if (domainEventEntity == null) break;

    //    domainEventEntity.IsPublished = true;
    //    await _domainEventService.Publish(domainEventEntity);
    //  }
    //}
  }
}
