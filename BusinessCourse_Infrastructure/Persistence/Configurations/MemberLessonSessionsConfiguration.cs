using BusinessCourse_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Configurations
{
  public class MemberLessonSessionsConfiguration : IEntityTypeConfiguration<MemberLessonSessions>
  {
    public void Configure(EntityTypeBuilder<MemberLessonSessions> builder)
    {
      builder.ToTable("Member_LessonSessions");
    }
  }
}
