using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogLib;

namespace FTPMonitor.Forms
{
    public class ProgressInfoEventArgs : EventArgs
    {
        private readonly MessageType mt;
        private readonly string name;

        /// <summary>
        /// 信息类型
        /// </summary>
        public MessageType MT { get { return mt; } }
        /// <summary>
        /// 复制完成的数据名
        /// </summary>
        public string Name { get { return name; } }

        public ProgressInfoEventArgs(MessageType mt, string name)
        {
            this.mt = mt;
            this.name = name;
        }
    }
}
