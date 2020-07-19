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

        public override void Receive(int size,Action<byte[]> callback)
        {
            Task.Factory.StartNew(() =>
            {
                byte[] buf = new byte[size];
                int totalLen = 0;
                while (totalLen<size)
                {
                    totalLen += m_Socket.Receive(buf, totalLen, size - totalLen, SocketFlags.None);
                }
                if (callback != null) callback(buf);
                return;
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
