using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket.Implt
{
    internal sealed class Server:Base.ServerBase
    {
        internal Server() : base(Tcp.TcpFactory.GetTcpSimple()) { }
    }
}
