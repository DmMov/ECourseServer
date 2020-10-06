using AutoMapper;
using ECourse.Application.Interfaces;

namespace ECourse.Application.Base
{
    public class HandlerBase
    {
        protected readonly IECourseContext context;
        protected readonly IMapper mapper;

        public HandlerBase(IECourseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
    }
}
