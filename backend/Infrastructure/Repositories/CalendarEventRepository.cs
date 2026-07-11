using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class CalendarEventRepository
    : GenericRepository<CalendarEvent>, ICalendarEventRepository
{
    public CalendarEventRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}