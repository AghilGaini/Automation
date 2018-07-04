using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Automation
{
    public partial class Default : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] SetPrivilges(List<Items> MenuItems)
        {
            try
            {
                string Result = "";
                bool InsertNew = false;
                bool Updated = false;

                if(CurrentUser.IsManager == true)
                {
                    foreach (var item in MenuItems)
                    {
                        var Privileg = Business.FacadeAutomation.GetPrivilegsBusiness().Privileges.Find(r => r.Gid == item.gid);

                        if (Privileg == null)
                        {
                            var p = new Data.Models.Generated.Automation.Privilge
                            {
                                Gid = item.gid,
                                Gref = item.gref,
                                Title = item.title
                            };

                            p.Save();
                            Business.FacadeAutomation.GetPrivilegsBusiness().Privileges.Add(p);
                            InsertNew = true;

                        }
                        else
                        {
                            if (Business.FacadeAutomation.GetPrivilegsBusiness().IsNeedUpdate(Privileg, item.title, item.gref))
                            {
                                Privileg.Gref = item.gref;
                                Privileg.Title = item.title;
                                Privileg.Save();
                                Updated = true;
                            }
                        }

                    }
                }
                

                if (InsertNew)
                    Result += "حقوق دسترسی جدید ثبت شد";

                if (Updated)
                    Result += "حقوق دسترسی به روز شد";

                if(InsertNew || Updated)
                    return new string[2] { "1", Result };

                return new string[2] { "2", Result };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion

        #region Classes

        public class Items
        {
            public Guid? gref { get; set; }
            public Guid gid { get; set; }
            public string title { get; set; }
        }

        #endregion
    }
}