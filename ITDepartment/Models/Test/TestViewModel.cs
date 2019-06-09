using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITDepartment.Models.Test
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public int TaskId { get; set; }
        public string Status { get; set; }
        public string TaskName { get; set; }
    }
}