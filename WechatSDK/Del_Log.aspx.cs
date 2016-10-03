using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WechatSDK
{
    public partial class Del_Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           string id= Request["ID"];
           Data da = new Data();

           da.DEL_Log(id);
           Response.Redirect("Admin.aspx");
        }
    }
}