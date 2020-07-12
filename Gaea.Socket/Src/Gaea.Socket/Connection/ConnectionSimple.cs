using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace GSocket.Connection {
    internal sealed class ConnectionSimple : ConnectionBase {
        internal ConnectionSimple(Socket s) : base(s) { }
        public override void BeginReceive()
        {
            Task.Factory.StartNew(() => {
                while (true)
                {
                    // 根据协议取前HEADER_LEN为包内容长度，不包含该HEADER_LEN
                    byte[] buf = new byte[4];
                    if (TryReceive(m_Socket, buf, 4, out string msg) <= 0)
                    {
                        if (m_actErrorHandler != null)
                        {
                            m_actErrorHandler(new ErrorEventArgs(-1, msg));
                            return;
                        }
                    }

                    int contentLength = BitConverter.ToInt32(buf, 0);
                    buf = new byte[contentLength];
                    if (TryReceive(m_Socket, buf, contentLength, out msg) <= 0)
                    {
                        if (m_actErrorHandler != null)
                        {
                            m_actErrorHandler(new ErrorEventArgs(-1, msg));
                            return;
                        }
                    }

                    Task.Factory.StartNew(() => {
                        m_actReceivedHandler(new ReceivedEventArgs(buf));
                    });
                }
            });
        }

        public override void Send(byte[] buf)
        {
            byte[] bufLen = new byte[4];
            BitConverter.GetBytes(buf.Length).CopyTo(bufLen, 0);
            byte[] bufPkg = new byte[4 + buf.Length];
            bufLen.CopyTo(bufPkg, 0);
            buf.CopyTo(bufPkg, 4);
            int len = TrySend(m_Socket, bufPkg, out string msg);
            if (len <= 0)
            {
                if (m_actErrorHandler == null) return;
                m_actErrorHandler(new ErrorEventArgs(-1, msg));
                return;
            }
            if (m_actSentHandler == null) return;
            m_actSentHandler(new SendEventArgs(len));
        }

        private int TryReceive(Socket s, byte[] buf, int size, out string msg)
        {
            // 存入接收缓冲区的偏移位置
            int offset = 0;
            msg = string.Empty;
            while (offset < size)
            {
                try
                {
                    int len = s.Receive(buf, offset, size - offset, SocketFlags.None);
                    if (len == 0)
                    {
                        msg = $"远程连接已关闭,远程地址:{((IPEndPoint)s.RemoteEndPoint).Address}";
                        s.Close();
                        return len;
                    }
                    offset += len;
                }
                catch (Exception e)
                {
                    msg = $"接收数据时出现异常,异常消息:{e.Message}";
                    return -1;
                }
            }
            return offset;
        }

        private int TrySend(Socket s, byte[] buf, out string msg)
        {
            msg = string.Empty;
            try
            {
                return s.Send(buf);
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
