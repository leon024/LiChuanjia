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
    public class SendMSG
    {
     string tokenValue = "";

    
        public string Send(string UserID, string Content)


        {

           


            string responeJsonStr = "{";
            responeJsonStr += "\"touser\": \"" + UserID + "\",";
            responeJsonStr += "\"msgtype\": \"text\",";
            responeJsonStr += "\"agentid\": \"0\",";
            responeJsonStr += "\"text\": {";
            responeJsonStr += "  \"content\": \"" + Content + "\"";
            responeJsonStr += "},";
            responeJsonStr += "\"safe\":\"0\"";
            responeJsonStr += "}";

            return SendQYMessage("wx8579b2dae40753d2", "JjVqfS47AEc9Z9UHhDvksIGZ_YPgW6Wjo80jGOTxKX8m8yn4VoHDaxRXJI43dIa5", responeJsonStr, Encoding.UTF8);

        
        
        
        
        
        }

        public class token
        {

            public string access_token { get; set; }
            public int expires_in { get; set; }

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












        public string SendQYMessage(string corpid, string corpsecret, string paramData, Encoding dataEncode)
        {
            string accessTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
            string accessTokenResult = HttpGet(accessTokenUrl, "corpid=wx8579b2dae40753d2&corpsecret=JjVqfS47AEc9Z9UHhDvksIGZ_YPgW6Wjo80jGOTxKX8m8yn4VoHDaxRXJI43dIa5");
            JavaScriptSerializer js = new JavaScriptSerializer();
            token mytoken = js.Deserialize<token>(accessTokenResult);
            tokenValue = mytoken.access_token;
            string postUrl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}", tokenValue);

            return PostWebRequest(postUrl, paramData, dataEncode);
        }




    }
}