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
    
    public partial class Role
    {
        public Role()
        {
            this.T_SYS_Permission = new HashSet<Permission>();
            this.T_SYS_UserRoleRelation = new HashSet<UserRoleRelation>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Available { get; set; }
        public Nullable<bool> IsSystemRole { get; set; }
        public string RoleDesc { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastModifier { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
    
        public virtual ICollection<Permission> T_SYS_Permission { get; set; }
        public virtual ICollection<UserRoleRelation> T_SYS_UserRoleRelation { get; set; }
    }
}