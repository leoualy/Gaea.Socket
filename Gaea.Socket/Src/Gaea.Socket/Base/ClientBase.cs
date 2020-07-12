using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;

namespace GSocket.Base
{
    internal abstract class ClientBase : Common, IClient
    {
        public void Connect(int port, string host)
        {
            Socket s =TcpSocket.CreateSocket();
            Task.Factory.StartNew(() =>
            {
                if (!TryConnect(s, host, port, out string msg)) {
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

        protected bool TryConnect(Socket s, string host, int port, out string msg)
        {
            msg = string.Empty;
            try
            {
                s.Connect(CreateRemoteEndPoint(port, host));
                return true;
            }
            catch (Exception e)
            {
                msg = $"尝试连接服务器时出现异常:{e.Message}";
                return false;
            }
        }
        protected EndPoint CreateRemoteEndPoint(int port, string host)
        {
            EndPoint p = new IPEndPoint(IPAddress.Parse(host), port);
            return p;
        }

    }
}
