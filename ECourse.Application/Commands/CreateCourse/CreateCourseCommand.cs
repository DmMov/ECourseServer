using AutoMapper;
using ECourse.Application.Base;
using ECourse.Application.Interfaces;
using ECourse.Application.Mappings;
using ECourse.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Commands.CreateCourse
{
    public sealed class CreateCourseCommand : IRequest<int>, IMapTo<Course>
    {
        public IFormFile File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int UserId { get; set; }

        public sealed class Handler : HandlerBase, IRequestHandler<CreateCourseCommand, int>
        {
            private readonly IFileService fileService;

            public Handler(IECourseContext context, IMapper mapper, IFileService fileService) : base(context, mapper)
            {
                this.fileService = fileService;
            }

            public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
            {
                Course course = mapper.Map<Course>(request);
                course.ImageName = await fileService.AddFileToDirectoryAsync(request.File, "Assets/Images");
                course.CreatedAt = DateTime.Now;

                context.Courses.Add(course);

                await context.SaveChangesAsync(cancellationToken);

                return course.Id;
            }
        }
    }
}
