using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace GSocket.Tcp {
    internal interface ITcp {
        Socket CreateSocket();
        bool Bind(Socket s, int port, out string msg, string host = null);
        bool TryConnect(Socket s, string host, int port,out string msg);
        /// <summary>
        /// 尝试开始接收数据
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        int TryReceive(Socket s, byte[] buf, int size, out string msg);
        /// <summary>
        /// 尝试发送数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        int TrySend(Socket s, byte[] buf, out string msg);
        /// <summary>
        /// 设置tcp数据包探测
        /// </summary>
        /// <param name="s">要设置的套接字</param>
        /// <param name="firstInterval">第一次启动的等待间隔</param>
        /// <param name="interval">两次探测的间隔</param>
        void OpenKeeplive(Socket s, int firstInterval, int interval);
    }
}
