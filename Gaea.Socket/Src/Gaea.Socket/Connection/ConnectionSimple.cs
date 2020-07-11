using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace GSocket.Connection {
    internal sealed class ConnectionSimple : ConnectionBase {
        public ConnectionSimple(Socket s) : base(s,new Tcp.TcpComplex()) { }
    }
}
