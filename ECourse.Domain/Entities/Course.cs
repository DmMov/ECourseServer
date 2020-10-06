using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Domain.Entities
{
    public sealed class Course
    {
        public Course()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public HashSet<Subscription> Subscriptions { get; set; }
    }
}
