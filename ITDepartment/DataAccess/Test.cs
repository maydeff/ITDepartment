//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITDepartment.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Test
    {
        public int TestId { get; set; }
        public int TaskId { get; set; }
        public string Status { get; set; }
    
        public virtual Task Task { get; set; }
    }
}
