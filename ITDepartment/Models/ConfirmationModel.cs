using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITDepartment.Models
{
    public class ConfirmationModel
    {
        public string ControllerName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ObjectType { get; set; }
    }
}