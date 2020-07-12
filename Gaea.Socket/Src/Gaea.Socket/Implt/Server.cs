using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GSocket.Base;
using GSocket.Connection;

namespace GSocket.Implt
{
    internal sealed class Server : ServerBase
    {
        protected override void Accept(Socket s)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Socket sAccept = s.Accept();
                    if (m_actConnectedHandler != null)
                    {
                        m_actConnectedHandler(new ConnectedEventArgs(GetConnection(sAccept)));
                    }
                }
            });
        }

        protected override IConnection GetConnection(Socket s)
        {
            return new ConnectionSimple(s);
        }
    }
}
