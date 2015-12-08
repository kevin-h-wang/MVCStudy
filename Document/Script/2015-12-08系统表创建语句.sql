/*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_User') 
     and   name  = 'Index_CnName' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_User.Index_CnName 
 go 

 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_User') 
     and   name  = 'Index_Account' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_User.Index_Account 
 go 

 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_User') 
     and   name  = 'Index_Available' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_User.Index_Available 
 go 

 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_User') 
     and   type = 'U') 
     drop table dbo.T_SYS_User 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_User ( 
  ID  uniqueidentifier  NOT NULL   ,
  CnName  Nvarchar(40)  NULL   ,
  EnName  Nvarchar(40)  NULL   ,
  Account  Nvarchar(40)  NULL   ,
  Password  Nvarchar(100)  NULL   ,
  Available  Bit  NULL   ,
  LoginFailCount  Int  NULL   ,
  LastLoginFailDate  DateTime  NULL   ,
  UserType  Int  NULL   ,
  Email  Nvarchar(40)  NULL   ,
  Tel  Nvarchar(40)  NULL   ,
  Mobile  Nvarchar(40)  NULL   ,
  GenderID  INT  NULL   ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_User ADD CONSTRAINT PK_T_SYS_User_ID PRIMARY KEY (ID) 
 go 

 ALTER TABLE T_SYS_User ADD CONSTRAINT T_SYS_User_Account unique (Account) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '用户表','user', 'dbo', 'table', 'T_SYS_User' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '主键标识','user', 'dbo', 'table', 'T_SYS_User', 'column', 'ID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '用户中文名称','user', 'dbo', 'table', 'T_SYS_User', 'column', 'CnName' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '用户英文名称','user', 'dbo', 'table', 'T_SYS_User', 'column', 'EnName' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '账号名','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Account' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '密码','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Password' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否可用','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Available' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '密码错误尝试次数','user', 'dbo', 'table', 'T_SYS_User', 'column', 'LoginFailCount' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后一次错误登录时间','user', 'dbo', 'table', 'T_SYS_User', 'column', 'LastLoginFailDate' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '人员类别','user', 'dbo', 'table', 'T_SYS_User', 'column', 'UserType' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   'Email','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Email' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '固定电话','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Tel' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '移动电话','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Mobile' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '性别','user', 'dbo', 'table', 'T_SYS_User', 'column', 'GenderID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_User', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_User', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_User', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_User', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
  CREATE INDEX Index_CnName  
  ON T_SYS_User (CnName) INCLUDE (Available)
 go 

  CREATE INDEX Index_Account  
  ON T_SYS_User (Account) INCLUDE (Available)
 go 

  CREATE INDEX Index_Available  
  ON T_SYS_User (Available) 
 go 

 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_Role') 
     and   type = 'U') 
     drop table dbo.T_SYS_Role 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_Role ( 
  ID  INT IDENTITY  NOT NULL  ,
  Name  Nvarchar(40)  NULL   ,
  Available  Bit  NULL   ,
  IsSystemRole  Bit  NULL   ,
  RoleDesc  Nvarchar(100)  NULL   ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_Role ADD CONSTRAINT PK_T_SYS_Role_ID PRIMARY KEY (ID) 
 go 

 ALTER TABLE T_SYS_Role ADD CONSTRAINT T_SYS_Role_Name unique (Name) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '角色表','user', 'dbo', 'table', 'T_SYS_Role' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '主键标识','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'ID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '角色名称','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'Name' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否可用','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'Available' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否是系统角色','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'IsSystemRole' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '角色描述','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'RoleDesc' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_Role', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_UserRoleRelation') 
     and   name  = 'Index_RoleID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_UserRoleRelation.Index_RoleID 
 go 

 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_UserRoleRelation') 
     and   name  = 'Index_UserID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_UserRoleRelation.Index_UserID 
 go 

 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_UserRoleRelation') 
     and   type = 'U') 
     drop table dbo.T_SYS_UserRoleRelation 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_UserRoleRelation ( 
  RoleID  Int IDENTITY NOT NULL   ,
  UserID  uniqueidentifier  NOT NULL   ,
  IsDefaultRole  Bit  NOT NULL DEFAULT 0 ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_UserRoleRelation ADD CONSTRAINT PK_T_SYS_UserRoleRelation_RoleIDUserID PRIMARY KEY (RoleID,UserID) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '用户角色关系表','user', 'dbo', 'table', 'T_SYS_UserRoleRelation' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '角色ID','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'RoleID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '用户ID','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'UserID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否默认角色','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'IsDefaultRole' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_UserRoleRelation', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
  CREATE INDEX Index_RoleID  
  ON T_SYS_UserRoleRelation (RoleID) INCLUDE (IsDefaultRole)
 go 

  CREATE INDEX Index_UserID  
  ON T_SYS_UserRoleRelation (UserID) INCLUDE (IsDefaultRole)
 go 

 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
  alter table dbo.T_SYS_UserRoleRelation  
    add constraint FK_T_SYS_UserRoleRelation_RoleID foreign key (RoleID) 
 references dbo.T_SYS_Role (ID) 
 go 

  alter table dbo.T_SYS_UserRoleRelation  
    add constraint FK_T_SYS_UserRoleRelation_UserID foreign key (UserID) 
 references dbo.T_SYS_User (ID) 
 go 

 /*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_Module') 
     and   name  = 'Index_ParentID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_Module.Index_ParentID 
 go 

 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_Module') 
     and   type = 'U') 
     drop table dbo.T_SYS_Module 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_Module ( 
  ID  INT IDENTITY  NOT NULL ,
  ParentID  Int  NOT NULL   DEFAULT 0 ,
  Name  Nvarchar(100)  NOT NULL   ,
  OrderNo  Numeric(24,4)  NULL   ,
  Description  Nvarchar(100)  NULL   ,
  Available  Bit  NULL   ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_Module ADD CONSTRAINT PK_T_SYS_Module_ID PRIMARY KEY (ID) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '模块表','user', 'dbo', 'table', 'T_SYS_Module' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '主键标识','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'ID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '父级ID','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'ParentID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   'ModuleName','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'Name' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '排序字段','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'OrderNo' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '描述','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'Description' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否可用','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'Available' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_Module', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
  CREATE INDEX Index_ParentID  
  ON T_SYS_Module (ParentID) 
 go 

 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_Action') 
     and   type = 'U') 
     drop table dbo.T_SYS_Action 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_Action ( 
  ID  Int IDENTITY NOT NULL  ,
  Name  Nvarchar(100)  NOT NULL   ,
  OrderNo  Numeric(24,4)  NULL   ,
  Description  Nvarchar(100)  NULL   ,
  Available  Bit  NULL   ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_Action ADD CONSTRAINT PK_T_SYS_Action_ID PRIMARY KEY (ID) 
 go 

 ALTER TABLE T_SYS_Action ADD CONSTRAINT T_SYS_Action_Name unique (Name) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '操作表','user', 'dbo', 'table', 'T_SYS_Action' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   'ID','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'ID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '操作名','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'Name' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '排序字段','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'OrderNo' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '描述','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'Description' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否可用','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'Available' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_Action', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
 /*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_Permission') 
     and   name  = 'Index_ModuleID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_Permission.Index_ModuleID 
 go 

 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_Permission') 
     and   name  = 'Index_ActionID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_Permission.Index_ActionID 
 go 

 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_Permission') 
     and   name  = 'Index_RoleID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_Permission.Index_RoleID 
 go 

 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_Permission') 
     and   type = 'U') 
     drop table dbo.T_SYS_Permission 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_Permission ( 
  ModuleID  Int  NOT NULL   ,
  ActionID  Int  NOT NULL   ,
  RoleID  Int  NOT NULL   ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_Permission ADD CONSTRAINT PK_T_SYS_Permission_ModuleIDActionIDRoleID PRIMARY KEY (ModuleID,ActionID,RoleID) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '授权表','user', 'dbo', 'table', 'T_SYS_Permission' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '模块操作Code','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'ModuleID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '操作ID','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'ActionID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '角色ID','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'RoleID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_Permission', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
  CREATE INDEX Index_ModuleID  
  ON T_SYS_Permission (ModuleID) 
 go 

  CREATE INDEX Index_ActionID  
  ON T_SYS_Permission (ActionID) 
 go 

  CREATE INDEX Index_RoleID  
  ON T_SYS_Permission (RoleID) 
 go 

 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
  alter table dbo.T_SYS_Permission  
    add constraint FK_T_SYS_Permission_ModuleID foreign key (ModuleID) 
 references dbo.T_SYS_Module (ID) 
 go 

  alter table dbo.T_SYS_Permission  
    add constraint FK_T_SYS_Permission_ActionID foreign key (ActionID) 
 references dbo.T_SYS_Action (ID) 
 go 

  alter table dbo.T_SYS_Permission  
    add constraint FK_T_SYS_Permission_RoleID foreign key (RoleID) 
 references dbo.T_SYS_Role (ID) 
 go 

 /*==============================================================*/ 
 /*  删除索引                                                    */ 
 /*==============================================================*/ 
 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_Menu') 
     and   name  = 'Index_ModuleID' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_Menu.Index_ModuleID 
 go 

 if exists (select 1 
     from  sysindexes 
    where  id    = object_id('dbo.T_SYS_Menu') 
     and   name  = 'Index_Available' 
     and   indid > 0 
     and   indid < 255) 
 drop index dbo.T_SYS_Menu.Index_Available 
 go 

 /*==============================================================*/ 
 /*  删除表                                                      */
 /*==============================================================*/ 
 if exists (select 1 
     from  sysobjects 
    where  id = object_id('dbo.T_SYS_Menu') 
     and   type = 'U') 
     drop table dbo.T_SYS_Menu 
 go 

 /*==============================================================*/ 
 /*  创建表                                                     */ 
 /*==============================================================*/ 
 create table dbo.T_SYS_Menu ( 
  ID  uniqueidentifier  NOT NULL   ,
  ModuleID  Int  NULL   ,
  Name  Nvarchar(100)  NULL   ,
  Level  Int  NULL   ,
  ParentID  uniqueidentifier  NULL   ,
  URL  Nvarchar(400)  NULL   ,
  LinkIcon  Nvarchar(100)  NULL   ,
  Available  Bit  NULL   ,
  MenuType  INT  NULL   ,
  Creator  Nvarchar(40)  NULL   ,
  CreateTime  DateTime  NULL   ,
  LastModifier  Nvarchar(40)  NULL   ,
  LastModifyTime  DateTime  NULL   )
 go 

 ALTER TABLE T_SYS_Menu ADD CONSTRAINT PK_T_SYS_Menu_ID PRIMARY KEY (ID) 
 go 

 /*==============================================================*/ 
 /*  添加备注                                                     */ 
 /*==============================================================*/ 
  execute sp_addextendedproperty 'MS_Description',  
   '菜单表','user', 'dbo', 'table', 'T_SYS_Menu' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '主键标识','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'ID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '模块ID','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'ModuleID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '菜单名','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'Name' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '菜单层级','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'Level' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '上一级菜单ID','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'ParentID' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '链接地址','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'URL' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '图标','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'LinkIcon' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '是否可用','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'Available' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '菜单类型','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'MenuType' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建人ID','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'Creator' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '创建日期','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'CreateTime' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改人','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'LastModifier' 
 go 

  execute sp_addextendedproperty 'MS_Description',   
   '最后修改日期','user', 'dbo', 'table', 'T_SYS_Menu', 'column', 'LastModifyTime' 
 go 

 /*==============================================================*/ 
 /*  创建索引                                                     */ 
 /*==============================================================*/ 
  CREATE INDEX Index_ModuleID  
  ON T_SYS_Menu (ModuleID) INCLUDE (Available)
 go 

  CREATE INDEX Index_Available  
  ON T_SYS_Menu (Available) 
 go 

 /*==============================================================*/ 
 /*  创建外键                                                     */ 
 /*==============================================================*/ 
  alter table dbo.T_SYS_Menu  
    add constraint FK_T_SYS_Menu_ModuleID foreign key (ModuleID) 
 references dbo.T_SYS_Module (ID) 
 go
