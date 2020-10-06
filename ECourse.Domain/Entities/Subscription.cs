using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Domain.Entities
{
    public sealed class Subscription
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}