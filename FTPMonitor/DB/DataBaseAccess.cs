using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace FTPMonitor.DB
{
    class DataBaseAccess
    {
        #region  成员变量
        /// <summary>
        /// 数据连接字符串
        /// </summary>
        private string connString;
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        protected SqlConnection Connection;

        private SqlDataAdapter adapter = new SqlDataAdapter();
        #endregion

        #region 构造方法
        /// <summary>
        /// 无参数的构造 
        /// </summary>
        public DataBaseAccess()
        {
            connString = Properties.Settings.Default.GFDataBaseConnectionString;
            Connection = new SqlConnection(connString);
        }
        #endregion

        #region 连接管理
        /// <summary>
        /// 打开连接
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
            }
            catch (Exception ex)
            {
                Connection.Close();
                Debug.Print(ex.Message);
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        #endregion

        #region  运行SQL语句
        /// <summary>
        /// 运行SQL语句，返回影响行数
        /// </summary>
        /// <param name="sqlString">SQl语句</param>
        /// <returns>影响行数</returns>
        public int RunSQLStringForUpdate(string sqlString)
        {
            Connection = new SqlConnection(connString);
            int RowAffected = 0;
            try
            {
                Connection.Open();
                SqlCommand command = new SqlCommand(sqlString, Connection);
                RowAffected = command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }
            return RowAffected;
        }
        /// <summary>
        /// 执行SQL语句，返回DataSet对象
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>DataSet对象</returns>
        public DataSet RunSQLStringForSelect(string sqlString)
        {
            Connection = new SqlConnection(connString);
            DataSet dst = new DataSet();
            try
            {
                Connection.Open();
                SqlDataAdapter oracleDataAdapter = new SqlDataAdapter();
                oracleDataAdapter.SelectCommand = new SqlCommand(sqlString, Connection);
                oracleDataAdapter.Fill(dst);
            }
            finally
            {
                Connection.Close();
            }
            return dst;
        }
        /// <summary>
        /// 执行SQL语句，返回DataTable对象
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>DataTable 对象</returns>
        public DataTable RunSQLStringWithSQLDataTable(string sqlString)
        {
            Connection = new SqlConnection(connString);
            DataSet dst = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                Connection.Open();
                SqlDataAdapter oracleDataAdapter = new SqlDataAdapter();
                oracleDataAdapter.SelectCommand = new SqlCommand(sqlString, Connection);
                oracleDataAdapter.Fill(dst);
                dt = dst.Tables[0];
            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }
        /// <summary>
        /// 执行包含有sum、count、max、min方法的sql语句，返回是一个Object对象
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <returns>返回Object对象</returns>
        public Object RunSQLStringScalar(string sqlString)
        {
            Connection = new SqlConnection(connString);
            Object returnObject = new Object();
            try
            {
                Connection.Open();
                SqlCommand command = new SqlCommand(sqlString, Connection);
                returnObject = (Object)command.ExecuteScalar();
            }
            finally
            {
                Connection.Close();
            }
            return returnObject;
        }

        /// <summary>
        /// 执行SQL语句，返回SqlDataAdapter对象
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>SqlDataAdapter 对象</returns>
        public SqlDataAdapter RunSQLWithAdapter(string sqlString)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                Connection.Open();
                adapter.SelectCommand = new SqlCommand(sqlString, Connection);
                return adapter;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行数据
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public SqlDataReader RunSQLStringWithDataReader(string sqlString)
        {
            SqlDataReader reader = null;
            try
            {
                Connection.Open();
                SqlCommand command = new SqlCommand(sqlString, Connection);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return reader;
            }

        }
        #endregion
    }
}
