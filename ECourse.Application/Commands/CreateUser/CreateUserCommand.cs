using AutoMapper;
using ECourse.Application.Base;
using ECourse.Application.Interfaces;
using ECourse.Application.Mappings;
using ECourse.Application.Models;
using ECourse.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Commands.CreateUser
{
    public sealed class CreateUserCommand : IRequest<string>, IMapTo<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IFormFile File { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => $"{x.FirstName}{x.LastName}"));
        }

        public sealed class Handler : IRequestHandler<CreateUserCommand, string>
        {
            private readonly IIdentityService identityService;
            private readonly IMapper mapper;
            private readonly IFileService fileService;

            public Handler(IIdentityService identityService, IMapper mapper, IFileService fileService)
            {
                this.identityService = identityService;
                this.mapper = mapper;
                this.fileService = fileService;
            }

            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                User user = mapper.Map<User>(request);

                if (request.File != null)
                    user.ImageName = await fileService.AddFileToDirectoryAsync(request.File, "Assets/Images");

                return (await identityService.CreateUserAsync(user, request.Password)).email;
            }
        }
    }
}
