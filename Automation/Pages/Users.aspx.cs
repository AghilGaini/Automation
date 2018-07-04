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
    public partial class Users : BasePage
    {
        #region Events
        
        protected void Page_Load(object sender, EventArgs e)
        {
            odsUsers.SelectCommand = Business.FacadeAutomation.GetUsersBusiness().GetAll().SQL;
        }
        
        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] Save(string info)
        {
            try
            {
                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                if (values["Username"].ToString() == "" || values["Name"].ToString() == "" || values["Family"].ToString() == "" || values["Email"].ToString() == "")
                    throw new Exception("اطلاعات ورودی کافی نیست");

                var Username = values["Username"].ToString();
                var Name = values["Name"].ToString();
                var Family = values["Family"].ToString();
                var Email = values["Email"].ToString();
                var Address = values["Address"].ToString();
                var Mobile = values["Mobile"].ToString();
                var ID = values["ID"].ToLong();

                if (ID == 0 && values["Password"].ToString() == "")
                    throw new Exception("اطلاعات ورودی کافی نیست");

                var UserInfo = Business.FacadeAutomation.GetUsersBusiness().GetByID(ID);

                if (UserInfo == null)
                    UserInfo = new Data.Models.Generated.Automation.User();

                UserInfo.Username = Username;
                UserInfo.Name = Name;
                UserInfo.Family = Family;
                UserInfo.Email = Email;
                UserInfo.Address = Address;
                UserInfo.Mobile = Mobile;
                UserInfo.IsActive = true;

                if (Business.FacadeAutomation.GetUsersBusiness().IsDuplicatedUsername(Username, ID) == true)
                    throw new Exception("نام کاربری تکراری است");

                if(ID == 0 )
                {
                    var password = values["Password"].ToString();
                    UserInfo.salt = Guid.NewGuid();
                    UserInfo.Password = MethodExtension.GetMd5Hash(password + UserInfo.salt);
                }

                UserInfo.Save();
                
                return new string[2] { "1", "عملیات با موفقیت انجام شد" };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        [WebMethod]
        public static string[] Get(long RowID)
        {
            try
            {
                var UserInfo = Business.FacadeAutomation.GetUsersBusiness().GetByID(RowID);

                if (UserInfo == null)
                    throw new Exception("موردی پیدا نشد");

                var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(UserInfo);

                return new string[2] { "1", jsonResult };

            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }
        
        #endregion
    }
}