using GSocket.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GSocket.Connection
{
    internal class ConnectionAsync : ConnectionBase
    {
        internal ConnectionAsync(Socket s) : base(s) { }
        public override void BeginReceive()
        {
            PostRecv(null);
        }

        public override void Send(byte[] buf)
        {
            PostSend(null);
        }

        private void IO_Completed(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                switch(e.LastOperation)
                {
                    case SocketAsyncOperation.Receive: Console.WriteLine(Encoding.UTF8.GetString(e.Buffer));break;
                    default:break;
                }
            }
        }

        private void PostRecv(SocketAsyncEventArgs e)
        {
            if (e == null)
            {
                byte[] buf = new byte[1024];
                e = new SocketAsyncEventArgs();
                e.SetBuffer(buf, 0, buf.Length);
                e.Completed += (o, rwe) =>
                {
                    IO_Completed(rwe);
                };
            }
            m_Socket.ReceiveAsync(e);
        }
        private void PostSend(SocketAsyncEventArgs e)
        {

        }

    }
}
