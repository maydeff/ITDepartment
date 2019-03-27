using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace ITDepartment.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizedAttribute : AuthorizeAttribute
    {
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
        private string[] _requiredRole = new string[0];

        public AuthorizedAttribute()
        {

        }
        public AuthorizedAttribute(string requiredRole)
        {
            _requiredRole = requiredRole.Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_userName == null || _userName == string.Empty) return false;
            if (_role == null || _role == string.Empty) return false;
            if (_requiredRole.Length == 0) return true;
            foreach (var role in _requiredRole)
            {
                if (role == _role)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var routeValues = new RouteValueDictionary();
            routeValues["controller"] = "Login";
            routeValues["action"] = "Index";
            filterContext.Result = new RedirectToRouteResult(routeValues);
        }
    }

}