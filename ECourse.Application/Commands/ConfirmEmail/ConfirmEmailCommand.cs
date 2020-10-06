using ECourse.Application.Exceptions;
using ECourse.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Commands.ConfirmEmail
{
    public sealed class ConfirmEmailCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string ConfirmationToken { get; set; }

        public sealed class Handler : IRequestHandler<ConfirmEmailCommand, bool>
        {
            private IIdentityService identityService;

            public Handler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                bool result = (await identityService.ConfirmEmailAsync(request.UserId, request.ConfirmationToken)).Succeeded;

                if (!result)
                    throw new BadRequestException("Email confirmation failure!");

                return true;
            }
        }
    }
}
