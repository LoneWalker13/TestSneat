using BusinessCourse_Application.Interfaces;
using BusinessCourse_Core.Entities;
using BusinessCourse_Core.Enum;
using FluentValidation.Resources;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCourse_Infrastructure.Persistence.Repository
{
  public class MemberLessonSessionsRepository : EFRepsitory<MemberLessonSessions>, IMemberLessonSessions
  {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public MemberLessonSessionsRepository(IConfiguration configuration, ApplicationDbContext context) : base(context)
    {
      _configuration = configuration;
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<MemberLessonSessions> GetMemberLessonSessionsById(int memberLessionSessionId)
    {
      var entity = await _context.MemberLessonSessions.FirstOrDefaultAsync(x => x.Id == memberLessionSessionId);
      return entity;
    }

    public List<GetMemberLessonSessionsList> GetMemberLessonSessionsList(string phoneNumber, string name, int lessonsId, int lessonSessionsId, Member_LessonSessionsAttendanceStatus attendanceStatus, Member_LessonSessionsPaymentStatus paymentStatus)
    {
      try
      {
        var parameters = new[] {
                new SqlParameter("@PhoneNumber", SqlDbType.VarChar,40) { Direction = ParameterDirection.Input, Value = phoneNumber == null ? DBNull.Value : phoneNumber},
                new SqlParameter("@Name", SqlDbType.VarChar,100) { Direction = ParameterDirection.Input, Value = name == null ? DBNull.Value : name},
                new SqlParameter("@LessonsId", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = lessonsId },
                new SqlParameter("@LessonSessionsId", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = lessonSessionsId },
                new SqlParameter("@AttendStatus", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (int)attendanceStatus },
                new SqlParameter("@PaymentStatus", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = (int)paymentStatus }
        };

        var result = _context.GetMemberLessonSessionsList.FromSqlRaw("[dbo].[GetMemberLessonSessionsList] @PhoneNumber,@Name, @LessonsId, @LessonSessionsId, @AttendStatus, @PaymentStatus", parameters).ToList();
        return result;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}

