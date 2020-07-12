using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace GSocket
{
    internal class TcpSocket
    {
        internal static bool Bind(Socket s, int port, out string msg, string host = null)
        {
            msg = string.Empty;
            try
            {
                IPEndPoint ipEPoint = new IPEndPoint(
                    host == null ? IPAddress.Any : IPAddress.Parse(host), port);
                s.Bind(ipEPoint);
                return true;
            }
            catch (Exception e)
            {
                msg = $"绑定主机和端口时错误:{e.Message}";
                return false;
            }
        }
        public static Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public static void OpenKeeplive(Socket s, int firstInterval, int interval)
        {
            byte[] optionInValue = new byte[12];
            BitConverter.GetBytes(1).CopyTo(optionInValue, 0);
            BitConverter.GetBytes(firstInterval).CopyTo(optionInValue, 4);
            BitConverter.GetBytes(interval).CopyTo(optionInValue, 8);
            s.IOControl(IOControlCode.KeepAliveValues, optionInValue, null);
        }
    }
}
