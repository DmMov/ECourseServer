using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECourse.Application.Interfaces
{
    public interface IHangfireJobService
    {
        Task SendTheDayBefore(string email, DateTime dayDate, string courseName, string userName);
        Task SendTheWeekBefore(string email, DateTime weekDate, string courseName, string userName);
        Task SendTheMonthBefore(string email, DateTime monthDate, string courseName, string userName);
    }
}
