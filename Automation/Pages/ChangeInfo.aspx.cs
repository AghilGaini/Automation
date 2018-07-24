using System;
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
    public partial class ChangeInfo : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region UploadFiles

        protected void fileUpload_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        {
            if (fileUpload.UploadedFiles.Count() > 0)
            {
                string sSavePath = "~/Pictures/Profiles/";
                string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
                string resultFileName = Path.ChangeExtension(MethodExtension.GetMd5Hash(CurrentUser.salt.ToString() + CurrentUser.ID), resultExtension);
                string resultFileUrl = sSavePath + resultFileName;
                string resultFilePath = MapPath(resultFileUrl);
                e.UploadedFile.SaveAs(resultFilePath);
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

                var Name = values["Name"].ToString();
                var Family = values["Family"].ToString();
                var Email = values["Email"].ToString();
                var Address = values["Address"].ToString();
                var Mobile = values["Mobile"].ToString();
                var UserID = CurrentUser.ID;

                var UserInfo = Business.FacadeAutomation.GetUsersBusiness().GetByID(UserID);

                UserInfo.Name = Name == "" ? UserInfo.Name : Name;
                UserInfo.Family = Family == "" ? UserInfo.Family : Family;
                UserInfo.Email = Email == "" ? UserInfo.Email : Email;
                UserInfo.Address = Address == "" ? UserInfo.Address : Address;
                UserInfo.Mobile = Mobile == "" ? UserInfo.Mobile : Mobile;

                UserInfo.Save();

                return new string[2] { "1", Resources.Texts.Success };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion
    }
}