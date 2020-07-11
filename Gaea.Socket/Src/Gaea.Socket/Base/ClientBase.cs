using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;


namespace GSocket.Base
{
    internal abstract class ClientBase : Common, IClient
    {
        #region 构造函数
        internal ClientBase() : base() { }
        internal ClientBase(Tcp.ITcp tcp) : base(tcp) { }
        #endregion

        public void Connect(int port, string host)
        {
            Socket s = m_Tcp.CreateSocket();
            Task.Factory.StartNew(() =>
            {
                if (!m_Tcp.TryConnect(s, host, port, out string msg)) {
                    if (m_actErrorHandler != null) {
                        m_actErrorHandler(new ErrorEventArgs(-1,msg));
                        return;
                    }
                }
                if (m_actConnectedHandler != null) {
                    m_actConnectedHandler(
                        new ConnectedEventArgs(GetConnection(s)
                        ));
                }
            });
        }

        
    }
}
