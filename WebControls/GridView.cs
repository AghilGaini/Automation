using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WebControls
{
    public class GridView : ASPxGridView
    {
        public GridView()
        {
            this.RightToLeft = DevExpress.Utils.DefaultBoolean.True;

            this.SettingsBehavior.ColumnResizeMode = DevExpress.Web.ASPxClasses.ColumnResizeMode.NextColumn;
            this.SettingsBehavior.AllowSort = true;
            this.SettingsBehavior.AllowSelectByRowClick = true;
            this.SettingsBehavior.AllowSelectSingleRowOnly = true;
            this.SettingsBehavior.AllowGroup = true;

            //this.SettingsText.GroupPanel = Resources.Texts.GroupPanelTitle;
            this.Settings.ShowGroupPanel = true;

            this.Styles.Header.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            this.Styles.Cell.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

            //this.SettingsText.EmptyDataRow = Resources.Texts.EmptyDataRow;
            this.SettingsCustomizationWindow.Enabled = true;
            this.Width = new Unit(100, UnitType.Percentage);

            this.CustomColumnDisplayText += new ASPxGridViewColumnDisplayTextEventHandler(Gridview_CustomColumnDisplayText);

            this.SettingsPager.ShowDefaultImages = true;
            this.SettingsPager.LastPageButton.Visible = true;
            this.SettingsPager.FirstPageButton.Visible = true;
            this.SettingsPager.FirstPageButton.Visible = true;
            this.SettingsPager.LastPageButton.Visible = true;
            this.SettingsPager.PageSize = 20;
            this.SettingsPager.Position = PagerPosition.Bottom;
            this.SettingsPager.AlwaysShowPager = true;
            this.SettingsPager.Visible = true;
            this.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            //this.SettingsPager.Summary.Text = Resources.Texts.SummaryText;
            this.Settings.ShowFilterRowMenu = true;
            this.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            this.Settings.ShowFilterRow = true;
            this.AutoGenerateColumns = false;
            this.Settings.ShowFilterBar = GridViewStatusBarMode.Auto;


            this.ClientSideEvents.SelectionChanged = "function(s, e){ if (e.isSelected) { $('#hdfRowID').val(s.GetRowKey(e.visibleIndex)); Get($('#hdfRowID').val()); Show_NewEdit();}}";
        }

        private void Gridview_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //if (e.Column.Caption == Resources.Texts.Row)
            //    e.DisplayText = (e.VisibleRowIndex + 1).ToString();
        }
    }
}
