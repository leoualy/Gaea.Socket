using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GSocket.Base
{
    internal abstract class ServerBase : Common, IServer
    {
        #region 构造函数
        internal ServerBase() : base() { }
        internal ServerBase(Tcp.ITcp tcp) : base(tcp) { }
        #endregion

        public bool Start(int port,out string err, string host = null) {
            Socket s = m_Tcp.CreateSocket();
            err = string.Empty;
            try {
                m_Tcp.Bind(s, port, out string msg, host);
                s.Listen(5);
            }
            catch(Exception e) {
                err = $"启动socket服务时出现异常:{e.Message}";
                return false;
            }
            m_ServerSocket = s;
            OnAccept();
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

        protected virtual void OnAccept() {
            Task.Factory.StartNew(() => {
                while (true) {
                    try {
                        Socket client = m_ServerSocket.Accept();
                        Task.Factory.StartNew(() => {
                            if (m_actConnectedHandler != null)
                                m_actConnectedHandler(new ConnectedEventArgs(GetConnection(client)));
                        });
                    }
                    catch(Exception e) {
                        if (m_actErrorHandler != null) {
                            m_actErrorHandler(new ErrorEventArgs(-1, $"Accept异常:{e.Message}"));
                        }
                    }
                }
            });
        }

        protected Socket m_ServerSocket;
    }
}
