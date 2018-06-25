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
   }
}
