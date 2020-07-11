using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSocket;
using System.Threading;



namespace ConsoleServer
{
    class Program
    {

        static void Main(string[] args)
        {
            IServer server = TcpProxy.GetServerAsync();
            //IServer server = TCPManager.GetServer();
            int count = 0;
            server.SetConnectedHandler((e) => {
                //if (e.StatusCode != 0) {
                //    Console.WriteLine(e.Msg);
                //    return;
                //}
                Console.WriteLine($"Connection Count is:{Interlocked.Increment(ref count)},Port is:{e.Conn.GetSourcePort()}");

                //e.Conn.SetReceivedCallback((re) => {
                //    Console.WriteLine(Encoding.UTF8.GetString(re.Buff));
                //});
                //e.Conn.BeginReceive();
            });
            server.Start(6000,out _);
            Console.WriteLine("start...");
            Console.Read();
        }
    }
}
