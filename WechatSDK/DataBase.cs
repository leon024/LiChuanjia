using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WechatSDK
{
    public class DataBase : IDisposable
    {
        #region 构造函数
        public DataBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion
        #region 创建连接对象
        private SqlConnection con = null;  //创建连接对
        #endregion
        #region   打开数据库连接
        /// <summary>
        /// 打开数据库连接.
        /// </summary>
        private void Open()
        {
            // 打开数据库连接
            if (con == null)
            {
                con = new SqlConnection("server=;database=lichuanjia.cn;uid=sa;pwd=123654");

                //  con = new SqlConnection("Data Source=localhost;Initial Catalog=EQNet;Integrated Security=True");
            }
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        #endregion
        #region  关闭连接
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (con != null && con.State != System.Data.ConnectionState.Closed)
            {
                con.Close();
            }
        }
        #endregion
        #region 释放数据库连接资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 确认连接是否已经关闭
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }
        #endregion
        #region   传入参数并且转换为SqlParameter类型
        /// <summary>
        /// 转换参数
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param></param>
        /// <param name="Size">参数大小</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary>
        /// 初始化参数值
        /// </summary>
        /// <param name="ParamName">存储过程名称或命令文本</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">参数方向</param>
        /// <param name="Value">参数值</param>
        /// <returns>新的 parameter 对象</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0) //指定参数大小
                param = new SqlParameter(ParamName, DbType, Size);
            else  //未指定大小
                param = new SqlParameter(ParamName, DbType);
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
                param.Value = Value;
            return param;
        }
        #endregion
        #region   执行参数命令文本(无数据库中数据返回)
        /// <summary>
        /// 执行数据库操作命令，有参数
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>

        public int RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);

            cmd.ExecuteNonQuery();
            this.Close();
            //得到执行成功返回值
            return (int)cmd.Parameters["ReturnValue"].Value;
        }
        /// <summary>
        /// 直接执行SQL语句，没有参数
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <returns></returns>
        public int RunProc(string procName)
        {
            this.Open();
            SqlCommand cmd = new SqlCommand(procName, con);
            int flag = cmd.ExecuteNonQuery();
            this.Close();
            return flag;
        }
        #endregion
        #region   执行参数命令文本(有返回值)
        /// <summary>
        /// 执行查询命令文本，并且返回DataSet数据集，有参数
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string procName, SqlParameter[] prams, string tbName)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, prams);
            DataSet ds = new DataSet();
            dap.Fill(ds, tbName);
            //this.Close();
            //得到执行成功返回值
            return ds;
        }
        /// <summary>
        /// 执行命令文本，并且返回DataSet数据集，无参数
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="tbName">数据表名称</param>
        /// <returns>DataSet</returns>
        public DataTable RunProcReturnDtable(string procName)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, null);
            DataTable dt = new DataTable();
            dap.Fill(dt);

            this.Close();
            //得到执行成功返回值
            return dt;
        }


        public DataSet RunProcReturnDS(string procName)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, null);
            DataSet ds = new DataSet();
            dap.Fill(ds);

            this.Close();
            //得到执行成功返回值
            return ds;
        }
        /// <summary>
        /// 执行命令文本，并且返回SqlDataReader数据集，无参数
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcReturn(string procName)
        {
            ///创建SqlCommand
            SqlCommand cmd = CreateCommand(procName, null);
            ///读取数据
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        /// <summary>
        /// 执行命令文本，并且返回SqlDataReader数据集，有参数
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public SqlDataReader RunProcReturn(string procName, SqlParameter[] prams)
        {
            ///创建SqlCommand
            SqlCommand cmd = CreateCommand(procName, prams);
            ///读取数据
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }
        #endregion
        #region 将命令文本添加到SqlDataAdapter
        /// <summary>
        /// 创建一个SqlDataAdapter对象以此来执行命令文本
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams">参数对象</param>
        /// <returns></returns>
        private SqlDataAdapter CreateDataAdaper(string procName, SqlParameter[] prams)
        {
            this.Open();
            SqlDataAdapter dap = new SqlDataAdapter(procName, con);
            dap.SelectCommand.CommandType = CommandType.Text;  //执行类型：命令文本
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    dap.SelectCommand.Parameters.Add(parameter);
            }
            //加入返回参数
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null));
            return dap;
        }
        #endregion
        #region   将命令文本添加到SqlCommand
        /// <summary>
        /// 创建一个SqlCommand对象以此来执行命令文本
        /// </summary>
        /// <param name="procName">命令文本</param>
        /// <param name="prams"命令文本所需参数</param>
        /// <returns>返回SqlCommand对象</returns>
        public void updtable(DataTable dt, string proc)
        {
            SqlDataAdapter dap = new SqlDataAdapter(proc, con);
            dap.Update(dt);



        }
        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            // 确认打开连接
            this.Open();
            SqlCommand cmd = new SqlCommand(procName, con);
            cmd.CommandType = CommandType.Text;　　　　 //执行类型：命令文本
            // 依次把参数传入命令文本
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }
            // 加入返回参数
            cmd.Parameters.Add(
                new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,
                string.Empty, DataRowVersion.Default, null));
            return cmd;
        }
        #endregion
    }

}