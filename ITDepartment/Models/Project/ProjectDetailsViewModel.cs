using System;
using System.ComponentModel.DataAnnotations;
using ITDepartment.Resources;

namespace ITDepartment.Models.Project
{
    public class ProjectDetailsViewModel : ProjectBaseViewModel
    {
        [Display(ResourceType = typeof(Text), Name = "ProjectDescription")]
        public string ProjectDescription { get; set; }
    }
}