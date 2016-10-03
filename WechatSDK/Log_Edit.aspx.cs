using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace WechatSDK
{
    public partial class Log_Edit : System.Web.UI.Page
    {
        static public string LogID;
        static public string tt;
        static public string ct;
        protected void Page_Load(object sender, EventArgs e)
        {
            LogID = Request["ID"];

            Data da = new Data();
            DataSet ds = new DataSet();
            ds = da.LogInfo(LogID);
            title.Value = ds.Tables[0].Rows[0][4].ToString();
            dscr.Value = ds.Tables[0].Rows[0][5].ToString();
            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.NameValueCollection nc = new System.Collections.Specialized.NameValueCollection(Request.Form);
            tt = nc.GetValues("title")[0].ToString();
            ct = nc.GetValues("dscr")[0].ToString();
            Data da = new Data();
            da.Update_Log(LogID,tt,ct);
            Response.Redirect("Admin.aspx");

        }
    }
}