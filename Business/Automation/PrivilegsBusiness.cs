using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class PrivilegsBusiness : BaseBusiness<Privilge>
    {
        private List<Privilge> _Privileges = new List<Privilge>();
   
        public List<Privilge> Privileges
        {
            get
            {
                if (_Privileges.Count == 0 || _Privileges == null)
                    _Privileges = this.GetAllList();

                return _Privileges;
            }
        }

        public List<Privilge> GetAllCache()
        {
            return Privileges;
        }

        public bool IsNeedUpdate(Privilge p , string Title , Guid? gref)
        {
            if (p.Title != Title || p.Gref != gref)
                return true;
            return false;
        }

    }
}
