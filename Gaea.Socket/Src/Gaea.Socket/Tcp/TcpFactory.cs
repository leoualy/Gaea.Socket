using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket.Tcp {
    internal class TcpFactory {
        internal static ITcp GetTcpSimple() {
            return new TcpSimple();
        }
    }
}
