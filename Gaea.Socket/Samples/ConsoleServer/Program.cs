using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSocket;
using System.Threading;
using System.Threading.Tasks;



namespace ConsoleServer
{
    class Program
    {

        static void Main(string[] args)
        {
            IServer server = TcpProxy.GetServerAsync();
            int count = 0;
            server.SetConnectedHandler((e) => {
                    Console.WriteLine($"Connection Count is:{Interlocked.Increment(ref count)},Port is:{e.Conn.GetSourcePort()}");
                e.Conn.BeginReceive();
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
