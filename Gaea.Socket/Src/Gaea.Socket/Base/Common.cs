using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GSocket.Base
{
    internal abstract class Common:AbsCallback
    {
        protected abstract IConnection GetConnection(Socket s);
    }
}
