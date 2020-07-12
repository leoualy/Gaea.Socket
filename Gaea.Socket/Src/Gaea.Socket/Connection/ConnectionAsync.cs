using GSocket.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using GSocket.Tcp;

namespace GSocket.Connection
{
    internal class ConnectionAsync : ConnectionBase
    {
        internal ConnectionAsync(Socket s):base(s,new TcpComplex()) { }
        public override void BeginReceive()
        {
            byte[] buf = new byte[1024];
            m_Tcp.TryReceive(m_Socket, buf, buf.Length, out _);
        }
    }
}
