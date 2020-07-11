using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using GSocket.EventArgs;

namespace GSocket
{
    public class ConnectedEventArgs:EventArgsBase {
        public ConnectedEventArgs(int statusCode, string msg) : base(statusCode, msg) {
            Conn = null;
        }
        public ConnectedEventArgs(IConnection conn) : base() {
            Conn = conn;
        }
        public IConnection Conn { get; private set; }
    }
}
