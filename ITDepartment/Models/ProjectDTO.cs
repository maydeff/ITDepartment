using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ITDepartment.Resources;

namespace ITDepartment.Models
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        [Display(ResourceType = typeof(Text), Name = "ProjectName")]
        public string ProjectName { get; set; }
        [Display(ResourceType = typeof(Text), Name = "ProjectDeadline")]
        public DateTime? ProjectDeadline { get; set; }

    }
}