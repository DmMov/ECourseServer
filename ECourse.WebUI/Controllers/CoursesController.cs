using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECourse.Application.Commands.CreateCourse;
using ECourse.Application.Commands.SubscribeToCourse;
using ECourse.Application.Queries.GetCourses;
using ECourse.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECourse.WebUI.Controllers
{
    public sealed class CoursesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ICollection<CourseVm>>> Get() =>
            Ok(await Mediator.Send(new GetCoursesQuery()));

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public async Task<ActionResult<ICollection<CourseVm>>> Create([FromForm] CreateCourseCommand command)
        {
            command.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Subscribe(SubscribeToCourseCommand command)
        {
            /*int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            command.UserId = userId;*/

            return Ok(await Mediator.Send(command));
        }
    }
}