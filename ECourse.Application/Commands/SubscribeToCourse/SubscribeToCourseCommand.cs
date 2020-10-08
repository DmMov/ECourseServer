using ECourse.Application.Exceptions;
using ECourse.Application.Interfaces;
using ECourse.Domain.Entities;
using ECourse.Templates;
using ECourse.Templates.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Commands.SubscribeToCourse
{
    public sealed class SubscribeToCourseCommand : IRequest<string>
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }

        public sealed class Handler : IRequestHandler<SubscribeToCourseCommand, string>
        {
            private readonly IECourseContext context;
            private readonly IMailSenderService mailSenderService;
            private readonly IHangfireJobService hangfireJobService;
            private readonly IRazorViewToStringRenderer renderer;

            const string view = "/Views/Emails/SuccessfullSubscription.cshtml";

            public Handler(
                IECourseContext context,
                IMailSenderService mailSenderService,
                IHangfireJobService hangfireJobService,
                IRazorViewToStringRenderer renderer
            )
            {
                this.context = context;
                this.mailSenderService = mailSenderService;
                this.hangfireJobService = hangfireJobService;
                this.renderer = renderer;
            }

            public async Task<string> Handle(SubscribeToCourseCommand request, CancellationToken cancellationToken)
            {
                bool isSubscribed = await context.Subscriptions
                    .AnyAsync(e => e.UserId == request.UserId && e.CourseId == request.CourseId);

                if (isSubscribed)
                    throw new BadRequestException("You have already subscribed to this course.");

                Course course = await context.Courses.FirstOrDefaultAsync(course => course.Id == request.CourseId);

                context.Subscriptions.Add(new Subscription
                {
                    UserId = request.UserId,
                    CourseId = request.CourseId,
                    StartDate = request.StartDate,
                    EndDate = request.StartDate.AddDays(course.Duration)
                });

                await context.SaveChangesAsync();

                Subscription subscription = await context.Subscriptions
                    .Include(x => x.Course)
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(e => e.UserId == request.UserId && e.CourseId == request.CourseId);

                string userFirstName = subscription.User.FirstName;
                string userLastName = subscription.User.LastName;

                SuccessfullSubscriptionVm model = new SuccessfullSubscriptionVm
                {
                    CourseName = course.Name,
                    UserName = $"{userFirstName} {userLastName}",
                    StartDate = $"{subscription.StartDate:MMMM dd}",
                    EndDate = $"{subscription.EndDate:MMMM dd}"
                };

                string body = await renderer.RenderViewToStringAsync(view, model);

                ConfigureBackgroundJob(
                    subscription.User.Email,
                    subscription.StartDate,
                    subscription.Course.Name,
                    $"{userFirstName} {userLastName}"
                );

                await mailSenderService.SendEmailAsync(subscription.User.Email, "ECourse | Course Subscription Notification", body);

                return "You've successfully subscribed!";
            }

            public void ConfigureBackgroundJob(string email, DateTime startDate, string courseName, string userName)
            {
                DateTime dayDate = startDate.AddDays(-1).Add(new TimeSpan(8, 0, 0));
                DateTime weekDate = startDate.AddDays(-7);
                DateTime monthDate = startDate.AddMonths(-1);

                if (weekDate > DateTime.Today)
                    hangfireJobService.SendTheWeekBefore(email, weekDate, courseName, userName);

                if (dayDate > DateTime.Today.Add(new TimeSpan(8, 0, 0)))
                    hangfireJobService.SendTheDayBefore(email, dayDate, courseName, userName);

                if (monthDate > DateTime.Today)
                    hangfireJobService.SendTheMonthBefore(email, monthDate, courseName, userName);
            }
        }
    }
}
