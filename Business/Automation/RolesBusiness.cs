using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class RolesBusiness : BaseBusiness<Role>
    {
        public Role GetByID(long ID)
        {
            try
            {
                var q = this.GetAll(1);
                q.And(Role.Columns.ID, ID);

                return this.Fetch(q).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
    }
}
