using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECourse.Application.ViewModels
{
    public sealed class LoginResultVm
    {
        public string JwtToken { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string ImageName { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string RefreshToken { get; set; }
    }
}
