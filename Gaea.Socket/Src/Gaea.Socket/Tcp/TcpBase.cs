using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GSocket.Tcp {
    internal abstract class TcpBase : ITcp {
        public bool Bind(Socket s, int port, out string msg, string host = null) {
            msg = string.Empty;
            try {
                IPEndPoint ipEPoint = new IPEndPoint(
                    host == null ? IPAddress.Any : IPAddress.Parse(host), port);
                s.Bind(ipEPoint);
                return true;
            }
            catch(Exception e) {
                msg = $"绑定主机和端口时错误:{e.Message}";
                return false;
            }
        }
        public Socket CreateSocket() {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void OpenKeeplive(Socket s, int firstInterval, int interval) {
            byte[] optionInValue = new byte[12];
            BitConverter.GetBytes(1).CopyTo(optionInValue, 0);
            BitConverter.GetBytes(firstInterval).CopyTo(optionInValue, 4);
            BitConverter.GetBytes(interval).CopyTo(optionInValue, 8);
            s.IOControl(IOControlCode.KeepAliveValues, optionInValue, null);
        }

        public virtual bool TryConnect(Socket s, string host, int port,out string msg) {
            msg = string.Empty;
            try {
                s.Connect(CreateRemoteEndPoint(port, host));
                return true;
            }
            catch(Exception e) {
                msg = $"尝试连接服务器时出现异常:{e.Message}";
                return false;
            }
        }

        public abstract int TryReceive(Socket s, byte[] buf, int size, out string msg);

        public abstract int TrySend(Socket s, byte[] buf, out string msg);
        protected EndPoint CreateRemoteEndPoint(int port, string host) {
            EndPoint p = new IPEndPoint(IPAddress.Parse(host), port);
            return p;
        }
    }
}
