using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSocket.Implt;


namespace GSocket
{
    public class TCPManager
    {
        public static IClient GetClient()
        {
            return new Client();
        }
        public static IServer GetServer()
        {
            return new Server();
        }
        public static IServer GetServerAsync() {
            return new ServerAysnc();
        }
    }
}
