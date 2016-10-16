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
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace WechatSDK
{
    public class Data


    {
        string connectionString = @"Server=localhost;Database=lichuanjia.cn;Uid=root;Pwd=123654;Port=3306;";


        public string Get_DateTime()
        {
          




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
           

            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            string sql2 = "insert into Image (FileName,Type,Date,Title,Describe) values('" + fm + "','jpg','" + Get_DateTime() + "','" + Title + "','" + Content + "')";

            string sql = "insert into Image (FileName,Type,Date,Title,ct) values('" + fm + "','jpg','" + Get_DateTime() + "','" + Title + "','" + Content + "')";
            MySqlCommand mysqlcom = new MySqlCommand(sql, con);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();

            return "1";



        }
        public string Add_Task_Log( string Title, string Content)
        {


            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();


            string sql="insert into Image (Date,Title,Describe) values('" + Get_DateTime() + "','" + Title + "','" + Content + "')";
            MySqlCommand mysqlcom = new MySqlCommand(sql, con);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();

            return "1";

        }
        public DataSet LogInfo(string LogID)
        {

            DataSet ds = new DataSet();
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            string commStr = "select * FROM Image   WHERE  ID= '" + LogID + "'";

            MySqlDataAdapter myadp = new MySqlDataAdapter(commStr, con); //适配器   
            myadp.Fill(ds); //将查询到数据填充到数据集  


            con.Close();
            return ds;


        }
        public string DEL_Log(string id)
        {


            

            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();


            string sql = "delete from Image where id='"+id+"'";


             MySqlCommand mysqlcom = new MySqlCommand(sql, con);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();


            return "1";




        }

        public string Update_Log(string id,string Title, string cc)
        {
            



            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();


            string sql = "update Image set Title='" + Title + "', ct='" + cc + "'  where ID='" + id + "'";


            MySqlCommand mysqlcom = new MySqlCommand(sql, con);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();


            return "1";


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

            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();



            DataSet ds = new DataSet();

            string selectStr2 = "select  * FROM  Image order by id desc";

            MySqlDataAdapter myadp = new MySqlDataAdapter(selectStr2, con); //适配器   
            myadp.Fill(ds); //将查询到数据填充到数据集  


            con.Close();

            return ds;




        }

       
    }
}