using GSocket.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using GSocket.Base;

namespace GSocket.Implt
{
    internal sealed class Client : ClientBase
    {
        protected override IConnection GetConnection(Socket s)
        {
            return new ConnectionSimple(s);
        }
    }
}
