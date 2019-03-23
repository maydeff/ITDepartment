using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ITDepartment.DataAccess;
using ITDepartment.Models;
using ITDepartment.Resources;

namespace ITDepartment.Controllers
{
    public class LoginController : Controller
    {
        private readonly ITDepartmentEntities context;

        public LoginController()
        {
            context = new ITDepartmentEntities();
        }

        // GET
        public ActionResult Index()
        {
            var formModel = new LoginFormModel();
            var allRoles = context.Role.ToList();

            foreach (var role in allRoles)
            {
                formModel.Roles.Add(new SelectListItem() { Selected = false, Text = role.RoleName, Value = role.RoleId.ToString()});
            }

            return View(formModel);
        }

        [HttpPost]
        public ActionResult Login(LoginFormModel model)
        {
            //TODO: password nie wypelnia sie po isnotvalid
            if (!ModelState.IsValid)
            {
                var allRoles = context.Role.ToList();

                foreach (var role in allRoles)
                {
                    model.Roles.Add(new SelectListItem() { Selected = false, Text = role.RoleName, Value = role.RoleId.ToString() });
                }
                return View("Index", model);
            }

            return null;
        }

    }
}