using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace Automation.Pages
{
    public partial class UploadImage : BasePage
    {
        string RootPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            RootPath =  Server.MapPath("~");

        }

        [WebMethod]

        public static string[] Get()
        {
            try
            {
                CurrentUser = Business.FacadeAutomation.GetUsersBusiness().GetByUsername("1");


                var RootPath = HostingEnvironment.MapPath("~/Pictures/Profiles");
                var FileName = MethodExtension.GetMd5Hash(CurrentUser.salt.ToString() + CurrentUser.ID) + ".*";

                var files = Directory.GetFiles(RootPath, FileName);

                if (files.Count() > 0)
                    return new string[2] { "1", Path.GetFileName(files[0]) };

                else
                    return new string[2] { "1", "Can not find" };

                


                return new string[2] { "1", RootPath };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        //protected void fileUpload_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
        //{
        //    if (fileUpload.UploadedFiles.Count() > 0)
        //    {
        //        string sSavePath = "~/Test/";
        //        string resultExtension = Path.GetExtension(e.UploadedFile.FileName);
        //        string resultFileName = Path.ChangeExtension("test", resultExtension);
        //        string resultFileUrl = sSavePath + resultFileName;
        //        string resultFilePath = MapPath(resultFileUrl);
        //        e.UploadedFile.SaveAs(resultFilePath);
        //    }
        //}

    }
}