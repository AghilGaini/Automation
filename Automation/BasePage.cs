using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities;

namespace Automation
{
    public class BasePage : System.Web.UI.Page
    {
        private bool? _NeedLogin;
        private bool? _IsDefault;

        public bool IsDefault
        {
            get
            {
                if (!_IsDefault.HasValue)
                    _IsDefault = false;
                return _IsDefault.Value;
            }
            set
            {
                _IsDefault = value;
            }
        }
        public bool NeedLogin
        {
            get
            {
                if (!_NeedLogin.HasValue)
                    _NeedLogin = true;
                return _NeedLogin.Value;
            }
            set
            {
                _NeedLogin = value;
            }
        }
        public Guid? gref { get; set; }
        public Guid gid { get; set; }
        public static Data.Models.Generated.Automation.User CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["USER_ID"] == null || HttpContext.Current.Session == null)
                    return null;
                var id = HttpContext.Current.Session["USER_ID"].ToLong();
                return Business.FacadeAutomation.GetUsersBusiness().GetByID(id);
            }
            set
            {
                if (value != null)
                    HttpContext.Current.Session["USER_ID"] = value.ID;
                else
                    HttpContext.Current.Session["USER_ID"] = null;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //without login
            if (NeedLogin && CurrentUser == null)
            {
                HttpContext.Current.Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            //Login but don't have permision to access to the page
            if (NeedLogin && !IsDefault && !Business.FacadeAutomation.GetVwUserPrivilegeRoleBusiness().HasPrivilege(CurrentUser, this.gid))
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx?AccessDenied=true");
                return;
            }
            base.OnLoad(e);
        }

        public void ShowException(Exception ex)
        {
            if (ex.Message == "-1000")
                Response.Redirect("~/Pages/Login.aspx");

            this.ShowUnSucceed(ex.Message);
        }

        private void ShowUnSucceed(string ErrorMessage)
        {
            var j = string.Format("$(document).ready(function() {{ShowError('','{0}'); $('.WaitMask').hide(); }});", ErrorMessage);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowException", j, true);
        }
        
    }
}