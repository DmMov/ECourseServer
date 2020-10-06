using ECourse.Application.Interfaces;
using System;

namespace ECourse.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
