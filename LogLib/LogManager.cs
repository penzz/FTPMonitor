using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogLib
{
    public class LogManager
    {
        #region 外部注册，显示日志
        public static event Action<string> ShowLogEvent;
        #endregion
        /// <summary>
        /// 字符串处理类
        /// </summary>
        private static StringBuilder stringBuilder = new StringBuilder();
        /// <summary>
        /// 记录日志消息
        /// </summary>
        private static log4net.ILog LogMessage = log4net.LogManager.GetLogger("LogMessage");

        #region 日志消息输出
        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        public static void AddMessage(MessageType messageType, string message)
        {
            stringBuilder.Clear();
            stringBuilder.Append("[" + DateTime.Now.ToString() + "]");
            stringBuilder.Append(" " + messageType.ToString() + ": " + message);
            //stringBuilder.Append(Environment.NewLine);
            WriteLogToFile(stringBuilder.ToString());
            ShowLogHandle();
        }
        /// <summary>
        /// 触发添加日志事件
        /// </summary>
        private static void ShowLogHandle()
        {
            if (ShowLogEvent != null)
            {
                ShowLogEvent(stringBuilder.ToString());
            }
        }
        /// <summary>
        /// 日志写入文件
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        private static void WriteLogToFile(string message)
        {
            if (LogMessage.IsInfoEnabled)
            {
                LogMessage.Info(message);
            }
        }
        #endregion
    }
    public enum MessageType
    {
        /// <summary>
        /// 
        /// </summary>
        NEW,
        /// <summary>
        /// 
        /// </summary>
        DEL,
        /// <summary>
        /// 
        /// </summary>
        INFO,
        /// <summary>
        /// 
        /// </summary>
        ERR,
        /// <summary>
        /// 
        /// </summary>
        EXIST,
        /// <summary>
        /// 
        /// </summary>
        COPY
    }
}
