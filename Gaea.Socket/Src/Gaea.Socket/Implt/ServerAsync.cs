using System.Threading.Tasks;
using System.Net.Sockets;
using GSocket.Base;
using System;
using GSocket.Connection;
using System.Threading;

namespace GSocket.Implt {
    internal class ServerAsync:ServerBase {
        private readonly int ACCEPT_COUNT =100;

        protected override void Accept(Socket s)
        {
            for (int i = 0; i < ACCEPT_COUNT; i++)
            {
                // 投递异步accept请求
                PostAccept();
            }
        }

        protected override IConnection GetConnection(Socket s)
        {
            return new ConnectionAsync(s);
        }

        private void AcceptCallback(SocketAsyncEventArgs e) {
            if (e.SocketError == SocketError.Success)
            {
                Socket s = e.AcceptSocket;
                Task.Factory.StartNew(() =>
                {
                    if (m_actConnectedHandler != null)
                    {
                        m_actConnectedHandler(new ConnectedEventArgs(GetConnection(s)));
                    }
                });
            }
            // 处理accept后,继续投递accept请求
            PostAccept(e);
        }

        private void PostAccept(SocketAsyncEventArgs eAccept=null)
        {
            if (eAccept == null)
            {
                eAccept = new SocketAsyncEventArgs();
                eAccept.Completed += (o, e) => {AcceptCallback(e); };
            }
            eAccept.AcceptSocket =TcpSocket.CreateSocket();
            
            if (!m_ServerSocket.AcceptAsync(eAccept))
            {
                // 返回false 则异步IO为立即完成
                AcceptCallback(eAccept);
            }
        }
    }
}
