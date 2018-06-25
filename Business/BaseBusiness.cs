using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Business
{
    public class BaseBusiness<T> where T : class
    {
        public string _TableName = PetaPoco.TableInfo.FromPoco(typeof(T)).TableName;
        public string _ConnectionStringName = "Automation";

        public Query GetAll(int? _topCount = null)
        {
            return new Query(this._TableName, _topCount);
        }

        public List<T> Fetch(Query q)
        {
            return new PetaPoco.Database(this._ConnectionStringName).Fetch<T>(q.q);
        }

        public List<T> GetAllList()
        {
            return this.Fetch(GetAll());
        }
    }
}
