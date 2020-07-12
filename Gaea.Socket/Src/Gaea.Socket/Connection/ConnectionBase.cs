using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GSocket.Base;

namespace GSocket.Connection {
    internal abstract class ConnectionBase :AbsCallback, IConnection {
        #region 构造函数
        internal ConnectionBase(Socket s)
        {
            m_Socket = s;
        }
        #endregion

        public abstract void BeginReceive();
        public abstract void Send(byte[] buf);

        public string GetSourceHost() {
            return ((IPEndPoint)m_Socket.RemoteEndPoint).Address.ToString();
        }

        public int GetSourcePort() {
            return ((IPEndPoint)m_Socket.RemoteEndPoint).Port;
        }
        protected Socket m_Socket;
    }
}
