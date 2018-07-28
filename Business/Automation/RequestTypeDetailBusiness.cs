using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class RequestTypeDetailBusiness : BaseBusiness<RequestTypeDetail>
    {
        public RequestTypeDetail GetByID(long ID)
        {
            var q = this.GetAll(1);
            q.And(RequestTypeDetail.Columns.ID, ID);

            return this.Fetch(q).FirstOrDefault();
        }

        public List<RequestTypeDetail> GetByTypeID(long TypeID)
        {
            var q = this.GetAll();
            q.And(RequestTypeDetail.Columns.RequestTypeID, TypeID);

            return this.Fetch(q);
        }
    }
}
