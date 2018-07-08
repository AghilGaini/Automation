using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Automation.Pages
{
    public partial class ChangePassword : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] Save(string info)
        {
            try
            {
                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                var CurrentPass = values["CurrentPass"].ToString();
                var NewPass = values["NewPass"].ToString();
                var ConfirmPass = values["ConfirmPass"].ToString();

                if (CurrentPass == "" || NewPass == "" || ConfirmPass == "")
                    throw new Exception("اطلاعات ورودی کافی نیست");

                if (NewPass != ConfirmPass)
                    throw new Exception("رمز جدید با رمز قبلی آن یکسان نیست");

                if (NewPass == CurrentPass)
                    throw new Exception("رمز فعلی با رمز جدید یکسان است");

                var UserInfo = Business.FacadeAutomation.GetUsersBusiness().GetByID(CurrentUser.ID);

                if (UserInfo == null)
                    throw new Exception("کاربری پیدا نشد");

                if (UserInfo.Password != MethodExtension.GetMd5Hash(CurrentPass + UserInfo.salt))
                    throw new Exception("رمز فعلی اشتباه است");

                UserInfo.Password = MethodExtension.GetMd5Hash(NewPass + UserInfo.salt);

                UserInfo.Save();

                return new string[2] { "1", "رمز عوض شد" };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion

    }
}