﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Automation.Pages
{
    public partial class Login : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUser = null;
        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] CheckLogin(string info)
        {
            try
            {

                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                var Username = values["Username"].ToString();
                var Password = values["Password"].ToString();

                if (Username == "" || Password == "")
                    throw new Exception("اطلاعات ورودی کافی نیست");

                var Userinfo = Business.FacadeAutomation.GetUsersBusiness().GetByUsername(Username);

                if (Userinfo == null)
                    throw new Exception("کاربری پیدا نشد");

                if (Userinfo.Password != MethodExtension.GetMd5Hash(Password + Userinfo.salt))
                    throw new Exception("رمز عبور اشباه است");

                CurrentUser = Userinfo;

                return new string[2] { "1", "Success" };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion
    }
}