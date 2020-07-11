using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace GSocket
{
    public interface IClient:IConnectedCallback,IErrorCallback
    {
        void Connect(int port, string host);
    }
}
