using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class UsersBusiness : BaseBusiness<User>
    {
        public User GetByID(long ID)
        {
            try
            {
                var q = this.GetAll(1);
                q.And(User.Columns.ID, ID);

                return this.Fetch(q).FirstOrDefault();
            }
            catch
            {
                
                throw;
            }
        }

        public User GetByUsername(string Username)
        {
            try
            {
                var q = this.GetAll(1);
                q.And(User.Columns.Username, Username);

                return this.Fetch(q).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
    }
}
