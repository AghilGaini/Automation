using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automation.Pages
{
    public partial class Users : BasePage
    {
        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            odsUsers.SelectCommand = Business.FacadeAutomation.GetUsersBusiness().GetAll().SQL;
        }
        
        #endregion

        #region WebMethod

        
        #endregion
    }
}