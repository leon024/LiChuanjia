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
using System.Linq;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace WechatSDK
{
    public partial class Add_Image : System.Web.UI.Page
    {
        public static string Flag = "0";

        public static string LogFlag;
        static string chinesename = "";
        static string username = "";
        static string  tokenValue = "";
        static string phone = "";
        static public string appId = "";
        static public string timestamp = "";
        static public string nonceStr = "";
        static public string signature = "";

        public static int imgcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                imgcount = 0;
                LogFlag = Guid.NewGuid().ToString();



           



            Hashtable ht = JSSDK.getSignPackage();
            appId = ht["appId"].ToString();
            nonceStr = ht["nonceStr"].ToString();
            timestamp = ht["timestamp"].ToString();
            signature = ht["signature"].ToString();


            string accessTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
            string accessTokenResult = HttpGet(accessTokenUrl, "corpid=wx8579b2dae40753d2&corpsecret=JjVqfS47AEc9Z9UHhDvksIGZ_YPgW6Wjo80jGOTxKX8m8yn4VoHDaxRXJI43dIa5");


            JavaScriptSerializer js = new JavaScriptSerializer();
            token mytoken = js.Deserialize<token>(accessTokenResult);
            tokenValue = mytoken.access_token;

  }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            imgcount++;
            System.Collections.Specialized.NameValueCollection nc = new System.Collections.Specialized.NameValueCollection(Request.Form);


            string mediaid = nc.GetValues("hide_file")[0].ToString();
            Image1.ImageUrl = mediaid;

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button2.Enabled = false;


            Data m = new Data();
            if (imgcount > 0)
            {
                
                System.Collections.Specialized.NameValueCollection nc = new System.Collections.Specialized.NameValueCollection(Request.Form);



                m.Add_Task_Log_File(nc.GetValues("serverid")[0].ToString(), title.Value.ToString(), dscr.Value.ToString(), tokenValue);

            }
            else
                m.Add_Task_Log( title.Value.ToString(), dscr.Value.ToString());


            Response.Redirect("default.aspx");


        }



        public class userinfo
        {
            public string UserId { get; set; }
            public string DeviceId { get; set; }
        }

        public class token
        {

            public string access_token { get; set; }
            public int expires_in { get; set; }

        }





        public string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ret;
        }
        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }



    }
}