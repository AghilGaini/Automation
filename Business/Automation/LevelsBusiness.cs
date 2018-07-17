using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class LevelsBusiness : BaseBusiness<Level>
    {
        public Level GetByID(long ID)
        {
            var q = this.GetAll(1);
            q.And(Level.Columns.ID, ID);

            return this.Fetch(q).FirstOrDefault();
        }

        public bool IsDuplicated(long ID , string Title)
        {
            var q = this.GetAll(1);
            q.And(Level.Columns.ID, CompareFilter.NotEqual, ID);
            q.And(Level.Columns.Title, Title);

            return this.Fetch(q).Any();
        }
    }
}
