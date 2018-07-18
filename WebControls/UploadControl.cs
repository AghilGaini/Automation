using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Web.ASPxUploadControl;

namespace WebControls
{
    public class UploadControl : ASPxUploadControl
    {
        public UploadControl()
        {
            this.BrowseButton.Text = "انتخاب فایل";
            this.UploadButton.Text = "بارگذاری";
        }
    }
}
