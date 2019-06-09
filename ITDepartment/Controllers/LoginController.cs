using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
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
            if (Session["UserName"] != null)
            {
                formModel.UserName = Session["UserName"].ToString();
            }

            formModel.Roles = GetRoleSelectList();

            return View(formModel);
        }

        [HttpPost]
        public ActionResult Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = GetRoleSelectList();
                return View("Index", model);
            }

            //Looking for user-role in the database
            using (var hashMethod = SHA512.Create())
            {
                var passwordHashed = hashMethod.ComputeHash(Encoding.GetEncoding(1250).GetBytes(model.Password));
                
                var user = context.User.FirstOrDefault(x => x.UserName == model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("Login", Text.LoginFailedMessage);
                    model.Roles = GetRoleSelectList();
                    return View("Index", model);
                }

                var userPass = Encoding.GetEncoding(1250).GetBytes(user.Password);
                if (!userPass.SequenceEqual(passwordHashed))
                {
                    ModelState.AddModelError("Login", Text.LoginFailedMessage);
                    model.Roles = GetRoleSelectList();
                    return View("Index", model);
                }

                var userRole = user.UserRole.FirstOrDefault(x => x.RoleId == model.SelectedRoleId);
                if (userRole == null)
                {
                    ModelState.AddModelError("Login", Text.LoginFailedMessage);
                    model.Roles = GetRoleSelectList();
                    return View("Index", model);
                }

                Session["UserName"] = user.UserName;
                Session["Role"] = userRole.Role.RoleName;

                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["Role"] = null;

            return RedirectToAction("Index", "Login");
        }
        public ActionResult GetForbiddenPage()
        {
            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        private IList<SelectListItem> GetRoleSelectList()
        {
            var toReturn = new List<SelectListItem>();
            var allRoles = context.Role.ToList();

            foreach (var role in allRoles)
            {
                toReturn.Add(new SelectListItem() { Selected = false, Text = role.RoleName, Value = role.RoleId.ToString() });
            }

            return toReturn;
        }

    }
}