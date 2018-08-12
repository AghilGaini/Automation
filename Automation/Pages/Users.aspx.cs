using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            odsLevel.SelectCommand = Business.FacadeAutomation.GetLevelsBusiness().GetAll().SQL;
        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] Save(string info)
        {
            try
            {
                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                var Username = values["Username"].ToString().ToLower();
                var Name = values["Name"].ToString();
                var Family = values["Family"].ToString();
                var Email = values["Email"].ToString();
                var Address = values["Address"].ToString();
                var Mobile = values["Mobile"].ToString();
                var RoleIDs = values["RoleIDs"] as ArrayList;
                var LevelID = values["LevelID"].ToLong();
                var ID = values["ID"].ToLong();

                if (Username == "" || Name == "" || Family == "" || Email == "")
                    throw new Exception(Resources.Texts.NotEnoughEntry);

                if (RoleIDs.Count == 0)
                    throw new Exception(Resources.Texts.RoleNotFound);

                if (ID == 0 && values["Password"].ToString() == "")
                    throw new Exception(Resources.Texts.NotEnoughEntry);

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
                UserInfo.LevelID = LevelID;

                if (Business.FacadeAutomation.GetUsersBusiness().IsDuplicatedUsername(Username, ID) == true)
                    throw new Exception(Resources.Texts.DuplicatedUsername);

                if (ID == 0)
                {
                    var password = values["Password"].ToString();
                    UserInfo.salt = Guid.NewGuid();
                    UserInfo.Password = MethodExtension.GetMd5Hash(password + UserInfo.salt);
                }

                UserInfo.Save();

                #region SaveRoles

                var NewUserRole = new List<Data.Models.Generated.Automation.UserRole>();

                foreach (var item in RoleIDs)
                {
                    var node = new Data.Models.Generated.Automation.UserRole();
                    node.UserID = UserInfo.ID;
                    node.RoleID = item.ToLong();
                    NewUserRole.Add(node);
                }

                Business.FacadeAutomation.GetSPBusiness().SP_DeleteOldRoles(UserInfo.ID);

                foreach (var item in NewUserRole)
                    item.Save();

                #endregion

                Business.FacadeAutomation.GetVwUserPrivilegeRoleBusiness().RefreshCache();

                return new string[2] { "1", Resources.Texts.Success };
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
                    throw new Exception(Resources.Texts.NotFound);

                var UserInfojsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(UserInfo);

                return new string[2] { "1", UserInfojsonResult };

            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion

        #region TreeList
        protected void TLRoles_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            TLRoles.UnselectAll();

            var Clear = (this.Master.FindControl("hdn") as WebControls.HiddenField).Get("Clear").ToBoolean();

            if (Clear == true)
                return;

            var UserID = (this.Master.FindControl("hdn") as WebControls.HiddenField).Get("RowID").ToLong();

            if (UserID == 0)
                throw new Exception(Resources.Texts.UserNotFound);

            var RoleIDs = Business.FacadeAutomation.GetUserRoleBusiness().GetByUserID(UserID).Select(r => r.RoleID).ToList();

            RoleIDs.ForEach(r => TLRoles.FindNodeByKeyValue(r.ToString().Replace("-", string.Empty)).Selected = true);
        }

        #endregion

        #region Method

        public List<Data.Models.Generated.Automation.Role> GetAll()
        {
            return Business.FacadeAutomation.GetRolesBusiness().GetAllList();
        }

        #endregion

    }
}