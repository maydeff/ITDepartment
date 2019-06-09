using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITDepartment.DataAccess;

namespace ITDepartment.Utilities
{
    public static class SessionHelper
    {
        public static string GetUsername()
        {
            return HttpContext.Current.Session["UserName"]?.ToString();
        }

        public static string GetRole()
        {
            return HttpContext.Current.Session["Role"]?.ToString();
        }

        public static bool CanView(string resourceName)
        {
            ITDepartmentEntities db = new ITDepartmentEntities();
            var resource = db.Resource.FirstOrDefault(x => x.ResourceName == resourceName);
            if (resource == null) return false;

            var roleName = GetRole();
            var resourceRole = db.ResourceRole.FirstOrDefault(x =>
                x.ResourceId == resource.ResourceId &&
                x.RoleId == db.Role.FirstOrDefault(y => y.RoleName == roleName).RoleId);
            return resourceRole == null ? false : resourceRole.CanView;
        }
    }
}