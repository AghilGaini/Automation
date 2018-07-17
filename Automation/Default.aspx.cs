using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Automation
{
    public partial class Default : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (Request.QueryString["AccessDenied"] != null && Request.QueryString["AccessDenied"].ToBoolean())
                    throw new Exception(Resources.Texts.AccessDenied);
            }
            catch (Exception ex)
            {
                this.ShowException(ex);
            }
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

                if (CurrentUser.IsManager == true)
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
                    Result += Resources.Texts.NewPrivilege;

                if (Updated)
                    Result += Resources.Texts.UpdatePrivilege;

                if (InsertNew || Updated)
                    return new string[2] { "1", Result };

                return new string[2] { "2", Result };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        [WebMethod]
        public static string[] GetPrivilge()
        {
            try
            {
                if (CurrentUser.IsManager == true)
                    return new string[2] { "1", Newtonsoft.Json.JsonConvert.SerializeObject(new string[1] { "manager" }) };

                var UserPrivilege = Business.FacadeAutomation.GetVwUserPrivilegeRoleBusiness().GetByUserID(CurrentUser.ID);

                return new string[2] { "1", Newtonsoft.Json.JsonConvert.SerializeObject(UserPrivilege.Select(r => r.Gid).ToList()) };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        [WebMethod]
        public static string[] GetInfo()
        {
            try
            {
                dynamic MyObject = new System.Dynamic.ExpandoObject();

                MyObject.Username = CurrentUser.Username;
                MyObject.Name = CurrentUser.Name;
                MyObject.Family = CurrentUser.Family;
                MyObject.Address = CurrentUser.Address;
                MyObject.Email = CurrentUser.Email;
                MyObject.Mobile = CurrentUser.Mobile;

                return new string[2] { "1", Newtonsoft.Json.JsonConvert.SerializeObject(MyObject) };
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