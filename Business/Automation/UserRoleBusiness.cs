using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class UserRoleBusiness : BaseBusiness<UserRole>
    {
        public List<UserRole> GetByUserID(long UserID)
        {
            var q = this.GetAll();
            q.And(UserRole.Columns.UserID, UserID);

            return this.Fetch(q);
        }
    }
}
