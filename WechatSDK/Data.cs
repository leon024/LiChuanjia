using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace WechatSDK
{
    public class Data
    {
        public string Get_DateTime()
        {
            DataBase db = new DataBase();




            DateTime Daytemp = DateTime.Now;
            string year = Daytemp.Year.ToString();
            string month = null;
            string day = null;
            month = Daytemp.Month.ToString();
            day = Daytemp.Day.ToString();
            if (Daytemp.Month.ToString().Length == 1)
                month = "0" + Daytemp.Month.ToString();

            if (Daytemp.Day.ToString().Length == 1)
                day = "0" + Daytemp.Day.ToString();


            return year + "/" + month + "/" + day + " " + DateTime.Now.ToLongTimeString();


        }

       
        public string Add_Task_Log_File( string FileName,string Title,string Content, string ACCESS_TOKEN)
        {

            string fm = GetMultimedia(ACCESS_TOKEN, FileName);
            fm = fm.Replace("D:\\LeonImage\\", "");
            DataBase db = new DataBase();


            int i = db.RunProc("insert into Image (FileName,Type,Date,Title,Describe) values('" + fm + "','jpg','" + Get_DateTime() + "','" + Title + "','" + Content + "')");

            return i.ToString();

        }
        public string Add_Task_Log( string Title, string Content)
        {

          
            DataBase db = new DataBase();


            int i = db.RunProc("insert into Image (Date,Title,Describe) values('" + Get_DateTime() + "','" + Title + "','" + Content + "')");

            return i.ToString();

        }
        public DataSet LogInfo(string LogID)
        {
            DataBase db = new DataBase();

            DataSet ds = new DataSet();


            string selectStr = "select * FROM Image   WHERE  ID= '" + LogID + "' ";
            ds = db.RunProcReturnDS(selectStr);
            return ds;


        }
        public string DEL_Log(string id)
        {


            DataBase db = new DataBase();


            int i = db.RunProc("delete from Image where id='"+id+"'");

            return i.ToString();

        }

        public string Update_Log(string id,string Title, string cc)
        {
            DataBase dbs = new DataBase();
          
            string selectStr = "update Image set Title='" + Title + "', Describe='" + cc + "'  where ID='" + id + "'";
            dbs.RunProc(selectStr);
            dbs.Dispose();

            return "ok";

        }
        private void MakeThumbnail(string sourcePath, string newPath, int width, int height)
        {
            System.Drawing.Image ig = System.Drawing.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(ig, new System.Drawing.Rectangle(0, 0, towidth, toheight), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }

        public string GetMultimedia(string ACCESS_TOKEN, string MEDIA_ID)
        {
            string file = string.Empty;
            string content = string.Empty;
            string strpath = string.Empty;
            string savepath = string.Empty;
            string stUrl = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=" + ACCESS_TOKEN + "&media_id=" + MEDIA_ID;
           
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(stUrl);

            req.Method = "GET";
            string newfile;
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();

                strpath = myResponse.ResponseUri.ToString();
                //WriteLog("接收类别://" + myResponse.ContentType);
                WebClient mywebclient = new WebClient();
                savepath = System.Web.HttpContext.Current.Server.MapPath("LeonImage") + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString().Substring(0, 4) + ".jpg";
                // WriteLog("路径://" + savepath);
                try
                {
                    mywebclient.DownloadFile(strpath, savepath);
                    file = savepath;
                }
                catch (Exception ex)
                {
                    savepath = ex.ToString();
                }
                 newfile = file.Replace("D:\\LeonImage\\", "");

            }
            MakeThumbnail(file, "D:\\LeonImage\\small_" + newfile, 250, 250);
            return file;
        }

        public DataSet Image_List()
        {

            DataBase db = new DataBase();



            DataSet ds = new DataSet();

            string selectStr2 = "select Top 50 * FROM  Image order by ID desc"; ;

            ds = db.RunProcReturnDS(selectStr2);

            return ds;




        }

       
    }
}