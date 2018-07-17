using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public static class FacadeAutomation
   {
       public static Automation.UsersBusiness GetUsersBusiness()
       {
           return new Automation.UsersBusiness();
       }

       public static Automation.PrivilegsBusiness GetPrivilegsBusiness()
       {
           return new Automation.PrivilegsBusiness();
       }

       public static Automation.RolesBusiness GetRolesBusiness()
       {
           return new Automation.RolesBusiness();
       }

       public static Automation.RolePrivilegesBusiness GetRolePrivilegesBusiness()
       {
           return new Automation.RolePrivilegesBusiness();
       }

       public static Automation.UserRoleBusiness GetUserRoleBusiness()
       {
           return new Automation.UserRoleBusiness();
       }

       public static Automation.VwUserPrivilegeRoleBusiness GetVwUserPrivilegeRoleBusiness()
       {
           return new Automation.VwUserPrivilegeRoleBusiness();
       }

       public static Automation.LevelsBusiness GetLevelsBusiness()
       {
           return new Automation.LevelsBusiness();
       }

       public static Automation.SPBusiness GetSPBusiness()
       {
           return new Automation.SPBusiness();
       }


   }
}
