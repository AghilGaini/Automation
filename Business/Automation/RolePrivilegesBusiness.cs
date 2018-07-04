using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class RolePrivilegesBusiness : BaseBusiness<RolePrivilge>
    {
        public List<RolePrivilge> GetByRoleID(long RoleID)
        {
            try
            {
                var q = this.GetAll();
                q.And(RolePrivilge.Columns.RoleID, RoleID);

                return this.Fetch(q);
            }
            catch
            {
                throw;
            }
        }
    }
}
