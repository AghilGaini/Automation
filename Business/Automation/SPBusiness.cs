using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Automation
{
    public class SPBusiness : BaseBusiness<dynamic>
    {
        public bool SP_DeleteOldPrivilege(long RoleID)
        {
            var result = new PetaPoco.Database(this._ConnectionStringName).Execute(";EXEC dbo.SP_DeleteOldPrivilege @@RoleID = @0", RoleID);
            if (result > 0)
                return true;
            return false;
        }
    }
}
