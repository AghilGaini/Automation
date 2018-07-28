using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class RequestTypesBusiness : BaseBusiness<RequestType>
    {

        public RequestType GetByID(long ID)
        {
            var q = this.GetAll(1);
            q.And(RequestType.Columns.ID, ID);

            return this.Fetch(q).FirstOrDefault();
        }

        public RequestType GetByTitle(string Title)
        {
            var q = this.GetAll(1);
            q.And(RequestType.Columns.Title, Title);

            return this.Fetch(q).FirstOrDefault();
        }

        public bool IsDuplicated(RequestType RType)
        {
            var q = this.GetAll(1);
            q.And(RequestType.Columns.Title, RType.Title);
            q.And(RequestType.Columns.ID, CompareFilter.NotEqual, RType.ID);

            return this.Fetch(q).Any();
        }

    }
}
