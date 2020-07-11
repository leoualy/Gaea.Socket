using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket.Implt
{
    internal sealed class Client:Base.ClientBase
    {
        internal Client() : base(Tcp.TcpFactory.GetTcpSimple()) { }
    }
}
