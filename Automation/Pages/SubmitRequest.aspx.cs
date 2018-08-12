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
    public partial class SubmitRequest : BasePage
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

                if (values["Description"].ToString() == "" || values["TypeID"].ToString() == "" )
                    throw new Exception(Resources.Texts.NotEnoughEntry);

                var Description = values["Description"].ToString();
                var ID = values["ID"].ToLong();
                var TypeID = values["TypeID"].ToLong();

                var RequestDosumnetInfo = Business.FacadeAutomation.GetRequestDocumentsBusiness().GetByID(ID);

                if (RequestDosumnetInfo == null)
                    RequestDosumnetInfo = new Data.Models.Generated.Automation.RequestDocument();

                if (ID != 0 && RequestDosumnetInfo.Sent == true)
                    throw new Exception(Resources.Texts.IsSentBefore);

                var RequestTypeDetail = Business.FacadeAutomation.GetRequestTypeDetailBusiness().GetByTypeID(TypeID);

                if (!RequestTypeDetail.Any(r => r.LevelID == CurrentUser.LevelID))
                    throw new Exception(Resources.Texts.CanNotCreateThisRequest);

                var Priority = RequestTypeDetail.First(r => r.LevelID == CurrentUser.LevelID);

                RequestDosumnetInfo.CreatedOn = DateTime.Now.ToString();
                RequestDosumnetInfo.CreatedBy = CurrentUser.Username;
                RequestDosumnetInfo.UserID = CurrentUser.ID;
                RequestDosumnetInfo.TypeID = TypeID;
                RequestDosumnetInfo.Description = Description;
                RequestDosumnetInfo.CurrentLevel = CurrentUser.LevelID;
                RequestDosumnetInfo.LevelPriority = Priority.Priority;
                RequestDosumnetInfo.Sent = false;
                RequestDosumnetInfo.Status = Resources.Texts.Status_Created;

                RequestDosumnetInfo.Save();


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
                var RequestDocumentInfo = Business.FacadeAutomation.GetRequestDocumentsBusiness().GetByID(RowID);

                if (RequestDocumentInfo == null)
                    throw new Exception(Resources.Texts.NotFound);

                return new string[2] { "1", Newtonsoft.Json.JsonConvert.SerializeObject(RequestDocumentInfo) };
            }
            catch (Exception ex)
            {
                return new string[2] { "0", ex.Message };
            }
        }

        #endregion

        #region Method

        public List<Data.Models.Generated.Automation.RequestType> GetAllRequests()
        {
            try
            {
                return Business.FacadeAutomation.GetRequestTypesBusiness().GetAllList();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Grid
        protected void grdRequets_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            grdRequets_CustomCallback(null, null);
        }

        protected void grdRequets_PageIndexChanged(object sender, EventArgs e)
        {
            grdRequets_CustomCallback(null, null);
        }

        protected void grdRequets_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            var RequestInformations = Business.FacadeAutomation.GetVwRequestFullInformationBusiness().GetByUserID(CurrentUser.ID);
            
            grdRequets.DataSource = RequestInformations;
            grdRequets.DataBind();
        }

        protected void grdRequets_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID != "btnSendG")
                    return;

                var RowID = grdRequets.GetRowValues(e.VisibleIndex, "RequestID").ToLong();

                var RequestDocument = Business.FacadeAutomation.GetRequestDocumentsBusiness().GetByID(RowID);

                if (RequestDocument == null)
                    throw new Exception(Resources.Texts.NotFound);

                if (RequestDocument.Sent == true)
                    throw new Exception(Resources.Texts.IsSentBefore);

                var RequestTypeDetails = Business.FacadeAutomation.GetRequestTypeDetailBusiness().GetByTypeID(RequestDocument.TypeID);

                var SelectOldLevel = RequestTypeDetails.FirstOrDefault(r => r.LevelID == RequestDocument.CurrentLevel && r.Priority == RequestDocument.LevelPriority);

                if (SelectOldLevel == null)
                    throw new Exception(Resources.Texts.NotFound);

                var Next = RequestTypeDetails.IndexOf(SelectOldLevel) + 1;

                if (Next >= RequestTypeDetails.Count)
                {
                    //TODO : این جا باید بگیم که تموم شه این درخواست(ارسال بشه و به معنی رسیدگی شده بشه) چون ممکنه همون اول کابری درخواست بده که توی مرحله آخر هست
                    throw new Exception(Resources.Texts.NoLevelForSend);
                }

                var SelectNewLevel = RequestTypeDetails[Next];

                RequestDocument.CurrentLevel = SelectNewLevel.LevelID;
                RequestDocument.LevelPriority = SelectNewLevel.Priority;
                RequestDocument.Sent = true;
                RequestDocument.Status = Resources.Texts.Status_Sent;

                RequestDocument.Save();

                grdRequets_CustomCallback(null, null);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion


    }
}