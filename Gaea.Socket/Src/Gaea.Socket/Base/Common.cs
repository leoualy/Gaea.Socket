using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using GSocket.Tcp;

namespace GSocket.Base
{
    internal abstract class Common:AbsCallback
    {
        #region 构造函数
        protected Common():this(new TcpSimple()) { }
        protected Common(ITcp tcp) {
            m_Tcp = tcp;
        }
        #endregion

        protected virtual IConnection GetConnection(Socket s) {
            return new Connection.ConnectionAsync(s);
        }

        protected ITcp m_Tcp;
    }
}
