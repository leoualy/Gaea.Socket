using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using GSocket.Base;
using GSocket.Connection;

namespace GSocket.Implt
{
    internal sealed class Server : ServerBase
    {
        protected override void Accept(Socket s)
        {
            
        }

        protected override IConnection GetConnection(Socket s)
        {
            return new ConnectionSimple(s);
        }
    }
}
