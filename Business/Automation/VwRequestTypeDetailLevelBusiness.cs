using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class VwRequestTypeDetailLevelBusiness : BaseBusiness<VwRequestTypeDetailLevel>
    {

        public List<VwRequestTypeDetailLevel> GetByTypeID(long TypeID)
        {
            var q = this.GetAll();
            q.And(VwRequestTypeDetailLevel.Columns.RequestTypeID, TypeID);

            return this.Fetch(q);
        }
    }
}
