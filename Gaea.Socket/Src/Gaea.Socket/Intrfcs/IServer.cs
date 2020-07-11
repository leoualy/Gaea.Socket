using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;


namespace GSocket
{
    public interface IServer:IConnectedCallback,IErrorCallback
    {
        /// <summary>
        /// 启动socket服务
        /// </summary>
        /// <param name="port"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        bool Start(int port,out string err, string host = null);
        /// <summary>
        /// 停止socket服务
        /// </summary>
        /// <returns></returns>
        bool Stop(out string err);
    }
}
