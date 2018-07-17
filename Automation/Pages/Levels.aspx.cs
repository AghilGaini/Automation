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
    public partial class Levels : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            odsLevels.SelectCommand = Business.FacadeAutomation.GetLevelsBusiness().GetAll().SQL;
        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] Get(long RowID)
        {
            try
            {
                var LevelInfo = Business.FacadeAutomation.GetLevelsBusiness().GetByID(RowID);

                if (LevelInfo == null)
                    throw new Exception(Resources.Texts.LevelNotFound);

                var JsonLevel = Newtonsoft.Json.JsonConvert.SerializeObject(LevelInfo);

                return new string[2] { "1", JsonLevel };

            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        [WebMethod]
        public static string[] Save(string info)
        {
            try
            {
                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                var Title = values["Title"].ToString();
                var ID = values["ID"].ToLong();

                if (Title == "")
                    throw new Exception(Resources.Texts.NotEnoughEntry);

                if (Business.FacadeAutomation.GetLevelsBusiness().IsDuplicated(ID, Title))
                    throw new Exception(Resources.Texts.DuplicatedLevel);

                var LevelInfo = Business.FacadeAutomation.GetLevelsBusiness().GetByID(ID);

                if (LevelInfo == null)
                    LevelInfo = new Data.Models.Generated.Automation.Level();

                LevelInfo.Title = Title;

                LevelInfo.Save();


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