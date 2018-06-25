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
        public Data.Models.Generated.Automation.User CurrentUser
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
                HttpContext.Current.Session["USER_ID"] = value.ID;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (NeedLogin && CurrentUser == null)
                HttpContext.Current.Response.Redirect("~/Pages/Login.aspx");
            base.OnLoad(e);
        }
    }
}