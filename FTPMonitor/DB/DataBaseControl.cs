using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FTPMonitor.DB
{
    class DataBaseControl
    {
        /// <summary>
        /// 操作数据库对象
        /// </summary>
        private static DataBaseAccess DBA = new DataBaseAccess();
        /// <summary>
        /// 加锁对象
        /// </summary>
        private static object obj = new object();

        #region  SQL语句相关操作
        /// <summary>
        /// 运行SQL语句，返回影响行数
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sqlString">SQl语句</param>
        /// <returns>影响行数</returns>
        public static int RunSqlForCount(string sqlString)
        {
            try
            {
                lock (obj)
                {
                    return DBA.RunSQLStringForUpdate(sqlString);
                }
            }
            catch (System.Exception ex)
            {
                return -1;
            }
        }
        /// <summary>
        /// 执行SQL语句，返回DataSet对象
        /// SqlDataAdapter.Fill(DataSet)
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>DataSet对象</returns>
        public static DataSet RunSqlForDataSet(string sqlString)
        {
            try
            {
                lock (obj)
                {
                    return DBA.RunSQLStringForSelect(sqlString);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 执行SQL语句，返回DataTable对象
        /// DataSet.Tables[0]
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>DataTable 对象</returns>
        public static DataTable RunSqlForDataTable(string sqlString)
        {
            try
            {
                lock (obj)
                {
                    return DBA.RunSQLStringWithSQLDataTable(sqlString);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 执行包含有sum、count、max、min方法的sql语句，返回是一个Object对象
        /// ExecuteScalar
        /// </summary>
        /// <param name="sqlString">sql语句</param>
        /// <returns>返回Object对象</returns>
        public static Object RunSqlForScalar(string sqlString)
        {
            try
            {
                lock (obj)
                {
                    return DBA.RunSQLStringScalar(sqlString);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回SqlDataAdapter对象
        /// </summary>
        /// <param name="sqlString">SQL语句</param>
        /// <returns>SqlDataAdapter 对象</returns>
        public static SqlDataAdapter RunSqlForAdapter(string sqlString)
        {
            try
            {
                lock (obj)
                {
                    return DBA.RunSQLWithAdapter(sqlString);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 执行数据
        /// ExecuteReader
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunSqlForDataReader(string sqlString)
        {
            try
            {
                lock (obj)
                {
                    return DBA.RunSQLStringWithDataReader(sqlString);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        #endregion
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void CloseConnection()
        {
            DBA.CloseConnection();
        }
    }
}
