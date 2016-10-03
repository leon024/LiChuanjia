using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace WechatSDK
{
    public partial class Finish_Close : System.Web.UI.Page
    {
        string tokenValue = "";
        static string phone = "";
        public string appId = "";
        public string timestamp = "";
        public string nonceStr = "";
        public string signature = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Hashtable ht = JSSDK.getSignPackage();
            appId = ht["appId"].ToString();
            nonceStr = ht["nonceStr"].ToString();
            timestamp = ht["timestamp"].ToString();
            signature = ht["signature"].ToString();
        }
    }
}