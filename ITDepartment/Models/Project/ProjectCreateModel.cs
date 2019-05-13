using System;
using System.ComponentModel.DataAnnotations;
using ITDepartment.Resources;

namespace ITDepartment.Models.Project
{
    public class ProjectCreateModel
    {
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