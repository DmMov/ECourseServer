using ECourse.Application.Exceptions;
using ECourse.Application.Interfaces;
using ECourse.Application.ViewModels;
using ECourse.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Commands.Login
{
    public sealed class LoginCommand : IRequest<LoginResultVm>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public sealed class Handler : IRequestHandler<LoginCommand, LoginResultVm>
        {
            private readonly ITokenService tokenService;
            private readonly IIdentityService identityService;

            public Handler(ITokenService tokenService, IIdentityService identityService)
            {
                this.tokenService = tokenService;
                this.identityService = identityService;
            }

            public async Task<LoginResultVm> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = identityService.GetUserAsync(request.Email).Result;

                if (!await identityService.ChekPasswordAsync(user, request.Password))
                    throw new BadRequestException("incorrect email or password.");

                await identityService.LoginAsync(user);

                if (!await identityService.IsEmailConfirmedAsync(user))
                    throw new BadRequestException("Unconfirmed email.");

                string token = tokenService.CreateToken(user);
                RefreshToken refreshToken = tokenService.GenerateRefreshToken("");

                user.RefreshTokens.Add(refreshToken);
                await identityService.UpdateUserAsync(user);

                return new LoginResultVm
                {
                    JwtToken = token,
                    UserName = user.UserName,
                    FullName = $"{user.FirstName} {user.LastName}",
                    ImageName = user.ImageName,
                    Role = await identityService.GetUserRoleAsync(user),
                    IsEmailConfirmed = true,
                    RefreshToken = refreshToken.Token
                };
            }
        }
    }
}
