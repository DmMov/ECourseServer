using AutoMapper;
using ECourse.Application.Mappings;
using ECourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Application.ViewModels
{
    public sealed class CourseVm : IMapFrom<Course>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int Duration { get; set; }
    }
}
