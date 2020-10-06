using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECourse.Application.Commands.CreateUser;
using ECourse.Application.Commands.Login;
using ECourse.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECourse.WebUI.Controllers
{
    public sealed class AuthController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<ActionResult<LoginResultVm>> Login(LoginCommand command) =>
            Ok(await Mediator.Send(command));
    }
}