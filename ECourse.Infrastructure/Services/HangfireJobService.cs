using Hangfire;
using ECourse.Templates.ViewModels;
using System;
using System.Threading.Tasks;
using ECourse.Templates;
using ECourse.Application.Interfaces;

namespace ECourse.Infrastructure.Services
{
    public class HangfireJobService : IHangfireJobService
    {
        private readonly IMailSenderService emailSenderService;
        private readonly IRazorViewToStringRenderer renderer;

        const string view = "/Views/Emails/CourseBeginingNotification.cshtml";

        public HangfireJobService(IMailSenderService emailSenderService, IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            this.emailSenderService = emailSenderService;
            renderer = razorViewToStringRenderer;
        }

        public async Task SendTheDayBefore(string email, DateTime dayDate, string courseName, string userName)
        {
            CourseBeginingVm model = new CourseBeginingVm
            {
                UserName = userName,
                Title = $"The \"{courseName}\" course begining notification",
                Message = $"The course \"{courseName}\" that you are subscribed to starts tomorrow",
            };

            string body = await renderer.RenderViewToStringAsync(view, model);

            BackgroundJob.Schedule(() =>
                emailSenderService.SendEmailAsync(email, "ECourse | Course Begining Notification", body), dayDate - DateTime.Today - DateTime.Now.TimeOfDay);
        }

        public async Task SendTheWeekBefore(string email, DateTime weekDate, string courseName, string userName)
        {
            CourseBeginingVm model = new CourseBeginingVm
            {
                UserName = userName,
                Title = $"The \"{courseName}\" course begining notification",
                Message = $"The course \"{courseName}\" that you are subscribed to starts in a week"
            };

            string body = await renderer.RenderViewToStringAsync(view, model);

            BackgroundJob.Schedule(() =>
                emailSenderService.SendEmailAsync(email, "ECourse | Course Begining Notification", body), weekDate - DateTime.Today - DateTime.Now.TimeOfDay);
        }

        public async Task SendTheMonthBefore(string email, DateTime monthDate, string courseName, string userName)
        {
            CourseBeginingVm model = new CourseBeginingVm
            {
                UserName = userName,
                Title = $"The \"{courseName}\" course begining notification",
                Message = $"The course \"{courseName}\" that you are subscribed to starts in a month"
            };

            string body = await renderer.RenderViewToStringAsync(view, model);

            BackgroundJob.Schedule(() =>
                emailSenderService.SendEmailAsync(email, "ECourse | Course Begining Notification", body), monthDate - DateTime.Today - DateTime.Now.TimeOfDay);
        }
    }
}
