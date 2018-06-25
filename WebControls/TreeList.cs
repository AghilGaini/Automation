using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Web.ASPxTreeList;

namespace WebControls
{
    public class TreeList : ASPxTreeList
    {
        public TreeList()
        {
            this.SettingsBehavior.ProcessSelectionChangedOnServer = false;
            this.SettingsSelection.AllowSelectAll = true;
            //this.SettingsSelection.Recursive = true;
            this.SettingsSelection.Enabled = true;
        }


    }
}
