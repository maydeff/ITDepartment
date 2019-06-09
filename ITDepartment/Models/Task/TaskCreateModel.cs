using ITDepartment.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITDepartment.Models.Task
{
    public class TaskCreateModel
    {
        public int TaskId { get; set; }
        public int? SprintId { get; set; }
        [Display(ResourceType = typeof(Text), Name = "TaskName")]
        public string TaskName { get; set; }
        [Display(ResourceType = typeof(Text), Name = "TaskDescription")]
        public string TaskDescription { get; set; }
        [Display(ResourceType = typeof(Text), Name = "TaskIsDone")]
        public bool IsDone { get; set; }
    }
}