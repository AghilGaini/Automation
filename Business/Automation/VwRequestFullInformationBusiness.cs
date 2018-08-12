using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class VwRequestFullInformationBusiness : BaseBusiness<VwRequestFullInoformation>
    {
        public List<VwRequestFullInoformation> GetByUserID(long UserID)
        {
            var q = this.GetAll();
            q.And(VwRequestFullInoformation.Columns.UserID, UserID);

            return this.Fetch(q);
        }
    }
}
