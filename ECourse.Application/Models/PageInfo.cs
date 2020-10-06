using System;
using System.Collections.Generic;
using System.Text;

namespace ECourse.Application.Models
{
    public class PageInfo
    {
        public int Current { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)Total / PageSize);
    }
}
