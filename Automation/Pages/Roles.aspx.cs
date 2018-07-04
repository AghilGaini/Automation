using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Automation.Pages
{
    public partial class Roles : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            odsRoles.SelectCommand = Business.FacadeAutomation.GetRolesBusiness().GetAll().SQL;
        }

        #endregion

        #region TreeList
        protected void TLPrivileges_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            try
            {
                TLPrivileges.UnselectAll();

                var Clear = (this.Master.FindControl("hdn") as WebControls.HiddenField).Get("Clear").ToLong();

                if (Clear == 1)
                    return;

                var RoleID = (this.Master.FindControl("hdn") as WebControls.HiddenField).Get("RowID").ToLong();

                var p = Business.FacadeAutomation.GetRolePrivilegesBusiness().GetByRoleID(RoleID);

                var Gids = Business.FacadeAutomation.GetPrivilegsBusiness().GetByIds(p.Select(r => r.PrivilegeID).ToList()).Select(r => r.Gid).ToList();

                Gids.ForEach(rp => TLPrivileges.FindNodeByKeyValue(rp.ToString().Replace("-", string.Empty)).Selected = true);
            }
            catch
            {
                throw;
            }

        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] Save(string info)
        {
            try
            {
                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                var RoleName = values["RoleName"].ToString();
                var RoleLevel = values["RoleLevel"].ToLong();
                var RoleID = values["RoleID"].ToLong();
                var Selected = values["Selected"] as ArrayList;

                if (RoleName == "" || RoleLevel == 0 || Selected.Count == 0)
                    throw new Exception("اطلاعات ورودی کافی نیست");

                var RoleInfo = Business.FacadeAutomation.GetRolesBusiness().GetByID(RoleID);

                if (RoleInfo == null)
                    RoleInfo = new Data.Models.Generated.Automation.Role();

                RoleInfo.RoleLevel = RoleLevel.ToLong();
                RoleInfo.RoleName = RoleName;

                var SelectedIDs = new List<long>();

                foreach (var item in Selected)
                {
                    var node = item as ArrayList;

                    var Temp = node[2].ToGuid();

                    while (Temp != Guid.Empty)
                    {
                        var Parent = Business.FacadeAutomation.GetPrivilegsBusiness().Privileges.Find(r => r.Gid == Temp.ToGuid());

                        if (!SelectedIDs.Any(r => r == Parent.ID))
                            SelectedIDs.Add(Parent.ID);

                        Temp = Parent.Gref.ToGuid();
                    }
                    if (!SelectedIDs.Any(r => r == node[0].ToLong()))
                        SelectedIDs.Add(node[0].ToLong());
                }

                RoleInfo.Save();

                var NewPrivilege = new List<Data.Models.Generated.Automation.RolePrivilge>();

                foreach (var item in SelectedIDs)
                {
                    var NewNode = new Data.Models.Generated.Automation.RolePrivilge();
                    NewNode.PrivilegeID = item;
                    NewNode.RoleID = RoleInfo.ID;
                    NewPrivilege.Add(NewNode);
                }

                Business.FacadeAutomation.GetSPBusiness().SP_DeleteOldPrivilege(RoleInfo.ID);

                foreach (var item in NewPrivilege)
                    item.Save();

                Business.FacadeAutomation.GetVwUserPrivilegeRoleBusiness().RefreshCache();


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
                var RoleInfo = Business.FacadeAutomation.GetRolesBusiness().GetByID(RowID);

                if (RoleInfo == null)
                    throw new Exception("موردی پیدا نشد");

                var RoleJson = Newtonsoft.Json.JsonConvert.SerializeObject(RoleInfo);

                return new string[2] { "1", RoleJson };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion

        #region Methods

        public List<Data.Models.Generated.Automation.Privilge> GetAllCache()
        {
            try
            {
                return Business.FacadeAutomation.GetPrivilegsBusiness().GetAllCache();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}