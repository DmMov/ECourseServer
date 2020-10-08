using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Templates.ViewModels
{
    public sealed class EmailButtonVm
    {
        public string Text { get; set; }
        public string Url { get; set; }

        public EmailButtonVm(string text, string url)
        {
            Text = text;
            Url = url;
        }
    }
}
