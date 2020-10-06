using ECourse.Application.Interfaces;
using ECourse.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECourse.Infrastructure
{
    public sealed class ECourseContextSeed
    {
        public async Task SeedDefaultUserAsync(UserManager<User> userManager)
        {
            User defaultUser = new User
            {
                UserName = "DmMov",
                FirstName = "Dmytro",
                LastName = "Movchaniuk",
                Email = "d@gmail.com",
                DateOfBirth = DateTime.Now.AddYears(-21),
                RegisteredDate = DateTime.Now
            };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "admin123!");
                await userManager.AddToRoleAsync(defaultUser, "admin");
            }
        }
        public async Task SeedRolesAsync(ECourseContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole<int> { Name = "admin", NormalizedName = "admin".ToUpper() },
                    new IdentityRole<int> { Name = "student", NormalizedName = "student".ToUpper() }
                );
            }

            await context.SaveChangesAsync();
        }

        public async Task SeedCoursesAsync(ECourseContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course
                    {
                        Name = "Modern React with Redux",
                        Description = "Master React v16.6.3 and Redux with React Router, Webpack, and Create-React-App. Includes Hooks!",
                        ImageName = "react-redux.png",
                        Duration = 7,
                        CreatedAt = DateTime.Now,
                        UserId = 1
                    },
                    new Course
                    {
                        Name = "Vue - The Complete Guide",
                        Description = "Vue.js is an awesome JavaScript Framework for building Frontend Applications! VueJS mixes the Best of Angular + React!",
                        ImageName = "vue.png",
                        Duration = 7,
                        CreatedAt = DateTime.Now,
                        UserId = 1
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}