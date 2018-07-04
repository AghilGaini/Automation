using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.Generated.Automation;

namespace Business.Automation
{
    public class VwUserPrivilegeRoleBusiness : BaseBusiness<VwUserPrivilegeRole>
    {
        public static List<VwUserPrivilegeRole> _VwUserPrivilegeRole = new List<VwUserPrivilegeRole>();
        public static DateTime ResetCache = DateTime.Now.AddMinutes(10);

        public List<VwUserPrivilegeRole> UserPrivilegeRole
        {
            get
            {
                if (_VwUserPrivilegeRole.Count == 0 || _VwUserPrivilegeRole == null || DateTime.Now.CompareTo(ResetCache) > 0)
                {
                    _VwUserPrivilegeRole = this.Fetch(this.GetAll());
                    ResetCache.AddMinutes(10);
                }
                return _VwUserPrivilegeRole;
            }
        }

        public void RefreshCache()
        {
            _VwUserPrivilegeRole = new List<VwUserPrivilegeRole>();
        }

    }
}
