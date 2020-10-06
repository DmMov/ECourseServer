using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Domain.Entities
{
    public sealed class User : IdentityUser<int>
    {
        public User()
        {
            Subscriptions = new HashSet<Subscription>();
            Courses = new HashSet<Course>();
            RegisteredDate = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegisteredDate { get; set; }

        public HashSet<Course> Courses { get; set; }
        public HashSet<Subscription> Subscriptions { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
