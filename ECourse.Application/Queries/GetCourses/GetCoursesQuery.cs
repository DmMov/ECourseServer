using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECourse.Application.Base;
using ECourse.Application.Interfaces;
using ECourse.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECourse.Application.Queries.GetCourses
{
    public sealed class GetCoursesQuery : IRequest<ICollection<CourseVm>>
    {
        public sealed class Handler : HandlerBase, IRequestHandler<GetCoursesQuery, ICollection<CourseVm>>
        {
            public Handler(IECourseContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ICollection<CourseVm>> Handle(GetCoursesQuery request, CancellationToken cancellationToken) =>
                await context.Courses
                    .OrderByDescending(x => x.CreatedAt)
                    .ProjectTo<CourseVm>(mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}
