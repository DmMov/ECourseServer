using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Templates.ViewModels
{
    public sealed class ConfirmEmailVm
    {
        public string UserName { get; set; }
        public string ConfirmationLink { get; set; }

        public ConfirmEmailVm(string userName, string confirmationLink)
        {
            UserName = userName;
            ConfirmationLink = confirmationLink;
        }
    }
}
