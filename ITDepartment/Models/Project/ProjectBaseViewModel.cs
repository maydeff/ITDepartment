using System;
using System.ComponentModel.DataAnnotations;
using ITDepartment.Resources;

namespace ITDepartment.Models.Project
{
    public class ProjectBaseViewModel
    {
        public int ProjectId { get; set; }
        [Display(ResourceType = typeof(Text), Name = "ProjectName")]
        public string ProjectName { get; set; }
        [Display(ResourceType = typeof(Text), Name = "ProjectDeadline")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "---")]
        public DateTime? ProjectDeadline { get; set; }
    }
}