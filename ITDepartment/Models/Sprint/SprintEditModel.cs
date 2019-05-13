using ITDepartment.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITDepartment.Models.Sprint
{
    public class SprintEditModel
    {
        public int SprintId { get; set; }
        [Display(ResourceType = typeof(Text), Name = "Project")]
        public int ProjectId { get; set; }
        [Display(ResourceType = typeof(Text), Name = "SprintStart")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "---")]
        public System.DateTime SprintStart { get; set; }
        //TODO: rename spint to sprint
        [Display(ResourceType = typeof(Text), Name = "SprintEnd")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "---")]
        public System.DateTime SpintEnd { get; set; }
    }
}