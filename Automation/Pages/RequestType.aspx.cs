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
    public partial class RequestType : BasePage
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            odsRequestType.SelectCommand = Business.FacadeAutomation.GetRequestTypesBusiness().GetAll().SQL;
        }

        #endregion

        #region WebMethod

        [WebMethod]
        public static string[] Save(string info)
        {
            try
            {
                var values = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, object>>(info);

                var Title = values["Title"].ToString();
                var Levels = values["RequestDetail"] as ArrayList;
                var ID = values["ID"].ToLong();

                var RequestTypeInfo = Business.FacadeAutomation.GetRequestTypesBusiness().GetByID(ID);

                if (RequestTypeInfo == null)
                    RequestTypeInfo = new Data.Models.Generated.Automation.RequestType();

                RequestTypeInfo.Title = Title;

                if (Business.FacadeAutomation.GetRequestTypesBusiness().IsDuplicated(RequestTypeInfo))
                    throw new Exception(Resources.Texts.DuplicatedTitle);

                RequestTypeInfo.Save();

                #region RequestTypeDetail

                Business.FacadeAutomation.GetSPBusiness().SP_DeleteOldTypeDetails(RequestTypeInfo.ID);

                List<long> LevelIDs = new List<long>();

                for (int i = 0; i < Levels.Count; i++ )
                {
                    var LevelInfo = Business.FacadeAutomation.GetLevelsBusiness().GetByTitle(Levels[i].ToString());
                    if (LevelInfo != null)
                        LevelIDs.Add(LevelInfo.ID);
                }

                for (int i = 0; i < LevelIDs.Count; i++ )
                {
                    var RequestDetails = new Data.Models.Generated.Automation.RequestTypeDetail();
                    RequestDetails.RequestTypeID = RequestTypeInfo.ID;
                    RequestDetails.LevelID = LevelIDs[i];
                    RequestDetails.Priority = i;
                    RequestDetails.Save();

                }

                #endregion

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
                var RequestType = Business.FacadeAutomation.GetRequestTypesBusiness().GetByID(RowID);
                if (RequestType == null)
                    throw new Exception(Resources.Texts.NotFound);

                var Details = Business.FacadeAutomation.GetVwRequestTypeDetailLevelBusiness().GetByTypeID(RequestType.ID);

                return new string[3] { "1", Newtonsoft.Json.JsonConvert.SerializeObject(RequestType), Newtonsoft.Json.JsonConvert.SerializeObject(Details) };

            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion

        #region Methods
        public List<Data.Models.Generated.Automation.Level> GetAllLevels()
        {
            try
            {
                return Business.FacadeAutomation.GetLevelsBusiness().GetAllList();
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}