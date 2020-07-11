
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public class TCPManager
    {
        public static IClient GetClient()
        {
            return new Implt.Client();
        }
        public static IServer GetServer()
        {
            return new Implt.Server();
        }
        public static IServer GetServerAsync() {
            return new Implt.ServerAysnc();
        }
    }
}
