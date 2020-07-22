using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GSocket.Base
{
    internal abstract class ServerBase:Common,IServer
    {
        public bool Start(int port,out string err, string host = null) {
            Socket s = TcpSocket.CreateSocket();
            err = string.Empty;
            try {
                TcpSocket.Bind(s, port, out string msg, host);
                s.Listen(5);
            }
            catch(Exception e) {
                err = $"启动socket服务时出现异常:{e.Message}";
                return false;
            }
            m_ServerSocket = s;
            Accept(s);
            return true;
        }

        public bool Stop(out string err) {
            err = string.Empty;
            try {
                m_ServerSocket.Close();
                return true;
            }
            catch(Exception e) {
                err = $"停止socket服务时出现异常:{e.Message}";
                return false;
            }
        }

        protected abstract void Accept(Socket s);

        protected Socket m_ServerSocket;
    }
}
