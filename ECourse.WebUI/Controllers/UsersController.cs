using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECourse.Application.Commands.ConfirmEmail;
using ECourse.Application.Commands.CreateUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECourse.WebUI.Controllers
{
    public sealed class UsersController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<string>> Create([FromForm] CreateUserCommand command) =>
            Ok(await Mediator.Send(command));

        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> ConfirmEmail(ConfirmEmailCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
