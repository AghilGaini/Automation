using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class RequestDocumentsBusiness : BaseBusiness<RequestDocument>
    {
        public RequestDocument GetByID(long ID)
        {
            var q = this.GetAll(1);
            q.And(RequestDocument.Columns.ID, ID);

            return this.Fetch(q).FirstOrDefault();
        }
    }
}
