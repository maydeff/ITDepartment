using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ITDepartment.Resources;

namespace ITDepartment.Models.Project
{
    public class ProjectEditModel
    {
        public int ProjectId { get; set; }
        [MaxLength(50, ErrorMessageResourceType = typeof(Text), ErrorMessageResourceName = "ProjectNameMaxLengthValidationMessage")]
        [Required]
        [Display(ResourceType = typeof(Text), Name = "ProjectName")]
        public string ProjectName { get; set; }
        [MaxLength(500, ErrorMessageResourceType = typeof(Text), ErrorMessageResourceName = "ProjectDescriptionMaxLengthValidationMessage")]
        [Required]
        [Display(ResourceType = typeof(Text), Name = "ProjectDescription")]
        public string ProjectDescription { get; set; }
        [Display(ResourceType = typeof(Text), Name = "ProjectDeadline")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "---")]
        public DateTime? ProjectDeadline { get; set; }
    }
}