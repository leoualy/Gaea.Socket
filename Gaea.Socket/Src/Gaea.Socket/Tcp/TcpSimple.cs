using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GSocket.Tcp {
    internal class TcpSimple : TcpBase {
        public override int TryReceive(Socket s, byte[] buf, int size, out string msg) {
            // 存入接收缓冲区的偏移位置
            int offset = 0;
            msg = string.Empty;
            while (offset < size) {
                try {
                    int len = s.Receive(buf, offset, size - offset, SocketFlags.None);
                    if (len == 0) {
                        msg = $"远程连接已关闭,远程地址:{((IPEndPoint)s.RemoteEndPoint).Address.ToString()}";
                        s.Close();
                        return len;
                    }
                    offset += len;
                } catch (Exception e) {
                    msg = $"接收数据时出现异常,异常消息:{e.Message}";
                    return -1;
                }
            }
            return offset;
        }

        public override int TrySend(Socket s, byte[] buf, out string msg) {
            msg = string.Empty;
            try {
                return s.Send(buf);
            }
            catch(Exception e) {
                return -1;
            }
        }
    }
}
