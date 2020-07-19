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
            int count = 0;
            server.SetConnectedHandler((e) => {
                    Console.WriteLine($"Connection Count is:{Interlocked.Increment(ref count)},Port is:{e.Conn.GetSourcePort()}");
                e.Conn.Receive(4,(buf)=>
                {
                    int len = BitConverter.ToInt32(buf, 0);
                    e.Conn.Receive(len, (cbuf) =>
                    {
                        Console.WriteLine(Encoding.UTF8.GetString(cbuf));
                    });
                });
            });
            server.Start(6000,out _);
            Console.WriteLine("start...");
            Console.Read();
        }
    }
}
