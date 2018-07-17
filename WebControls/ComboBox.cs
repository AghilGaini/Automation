using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Web.ASPxEditors;

namespace WebControls
{
    public class ComboBox : ASPxComboBox
    {
        public ComboBox()
        {
            this.DropDownStyle = DropDownStyle.DropDownList;
            this.IncrementalFilteringDelay = 10;
            this.EnableIncrementalFiltering = true;
            this.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
            this.SettingsLoadingPanel.Enabled = true;
            this.SettingsLoadingPanel.Text = "در حال آماده سازی";
            this.SettingsLoadingPanel.ShowImage = true;
            this.SettingsLoadingPanel.ImagePosition = DevExpress.Web.ASPxClasses.ImagePosition.Right;
            this.Width = new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage);
            this.Height = 30;
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            this.ValidationSettings.Display = Display.Dynamic;
        }

        public override void DataBind()
        {
            base.DataBind();

            if (this.Columns.Count <= 1)
            {
                ListEditItem li = new ListEditItem();
                li.Text = "انتخاب";
                li.Value = null;
                this.Items.Insert(0, li);
            }
            else
            {
                this.DropDownStyle = DropDownStyle.DropDown;
            }
        }
    }
}
