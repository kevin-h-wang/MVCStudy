//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyUIDemo.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRoleRelation
    {
        public int RoleID { get; set; }
        public System.Guid UserID { get; set; }
        public bool IsDefaultRole { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastModifier { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
    
        public virtual Role T_SYS_Role { get; set; }
        public virtual User T_SYS_User { get; set; }
    }
}
