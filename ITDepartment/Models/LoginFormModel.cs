using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ITDepartment.Resources;

namespace ITDepartment.Models
{
    public class LoginFormModel
    {
        [Display(ResourceType = typeof(Resources.Text), Name = "Username")]
        [Required(ErrorMessageResourceName = "UsernameValidationMessage", ErrorMessageResourceType = typeof(Resources.Text))]
        public string UserName { get; set; }

        [Display(ResourceType = typeof(Resources.Text), Name = "Passsword")]
        [Required(ErrorMessageResourceType = typeof(Resources.Text), ErrorMessageResourceName = "PasswordValidationMessage")]
        public string Password { get; set; }
        [Display(ResourceType = typeof(Resources.Text), Name = "Role")]
        public IList<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessageResourceType = typeof(Resources.Text), ErrorMessageResourceName = "RoleValidationMessage")]
        public int SelectedRoleId { get; set; }
        

    }
}