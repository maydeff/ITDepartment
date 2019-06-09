using ITDepartment.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ITDepartment.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class AuthorizedAttribute : AuthorizeAttribute
    {
        //Helper flag for testing purposes
        private bool _authorizationMode = true;
        private string _operationType;
        private string _resourceName;
        private ITDepartmentEntities db = new ITDepartmentEntities();
        private bool _redirectToLoginPage = false;
        private string _userName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] == null
                    ? null
                    : HttpContext.Current.Session["UserName"].ToString();
            }
        }

        private string _role
        {
            get
            {
                return HttpContext.Current.Session["Role"] == null
                    ? null
                    : HttpContext.Current.Session["Role"].ToString();
            }
        }

        public AuthorizedAttribute()
        {

        }
        public AuthorizedAttribute(string resourceName, string operationType)
        {
            _operationType = operationType;
            _resourceName = resourceName;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_authorizationMode == false)
            {
                return true;
            }

            if (_operationType == null && _resourceName == null)
            {
                var ifLoggedIn = _role != null && _userName != null;
                if (!ifLoggedIn)
                {
                    _redirectToLoginPage = true;
                }

                return ifLoggedIn;
            }

            if (_userName == null || _userName == string.Empty) return false;
            if (_role == null || _role == string.Empty) return false;


            var resource = db.Resource.FirstOrDefault(x => x.ResourceName == _resourceName);
            if (resource == null) return false;
            var resourceRole = db.ResourceRole.FirstOrDefault(x =>
                x.ResourceId == resource.ResourceId &&
                x.RoleId == db.Role.FirstOrDefault(y => y.RoleName == _role).RoleId);
            if (resourceRole == null) return false;
            switch (_operationType)
            {
                case "Add":
                    return resourceRole.CanAdd;
                case "View":
                    return resourceRole.CanView;
                case "Edit":
                    return resourceRole.CanEdit;
                case "Delete":
                    return resourceRole.CanDelete;
                default:
                    return false;
            }

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var routeValues = new RouteValueDictionary();

            if (_redirectToLoginPage)
            {
                routeValues["controller"] = "Login";
                routeValues["action"] = "Index";
            }
            else
            {
                routeValues["controller"] = "Login";
                routeValues["action"] = "GetForbiddenPage";
            }

            filterContext.Result = new RedirectToRouteResult(routeValues);

        }
    }

}