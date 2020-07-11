using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GSocket.Tcp;

namespace GSocket.Connection {
    internal abstract class ConnectionBase :Base.AbsCallback, IConnection {
        #region 构造函数
        internal ConnectionBase(Socket s, ITcp tcp) {
            m_Socket = s;
            m_Tcp = tcp;
        }
        internal ConnectionBase(Socket s) : this(s, TcpFactory.GetTcpSimple()) { }
        #endregion

        public void BeginReceive() {
            Task.Factory.StartNew(() => {
                while (true) {
                    // 根据协议取前HEADER_LEN为包内容长度，不包含该HEADER_LEN
                    byte[] buf = new byte[4];
                    if(m_Tcp.TryReceive(m_Socket, buf, 4, out string msg)<=0) {
                        if (m_actErrorHandler != null) {
                            m_actErrorHandler(new ErrorEventArgs(-1, msg));
                            return;
                        }
                    }

                    int contentLength= BitConverter.ToInt32(buf, 0);
                    buf = new byte[contentLength];
                    if(m_Tcp.TryReceive(m_Socket,buf,contentLength,out msg)<=0) {
                        if (m_actErrorHandler != null) {
                            m_actErrorHandler(new ErrorEventArgs(-1, msg));
                            return;
                        }
                    }

                    Task.Factory.StartNew(() => {
                        m_actReceivedHandler(new ReceivedEventArgs(buf));
                    });
                }
            });
        }

        public void Send(byte[] buf) {
            byte[] bufLen = new byte[4];
            BitConverter.GetBytes(buf.Length).CopyTo(bufLen, 0);
            byte[] bufPkg = new byte[4 + buf.Length];
            bufLen.CopyTo(bufPkg, 0);
            buf.CopyTo(bufPkg, 4);
            int len = m_Tcp.TrySend(m_Socket, bufPkg, out string msg);
            if (len<=0) {
                if (m_actErrorHandler == null) return;
                m_actErrorHandler(new ErrorEventArgs(-1, msg));
                return;
            }
            if (m_actSentHandler == null) return;
            m_actSentHandler(new SendEventArgs(len));
        }

        public string GetSourceHost() {
            return ((IPEndPoint)m_Socket.RemoteEndPoint).Address.ToString();
        }

        public int GetSourcePort() {
            return ((IPEndPoint)m_Socket.RemoteEndPoint).Port;
        }

        protected ITcp m_Tcp;
        protected Socket m_Socket;
    }
}
