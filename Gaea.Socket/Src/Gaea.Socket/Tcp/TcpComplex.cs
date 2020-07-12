using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GSocket.Tcp {
    internal class TcpComplex : TcpBase {
        private void IO_Completed(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                Console.WriteLine(Encoding.UTF8.GetString(e.Buffer));
            }
        }
        public override int TryReceive(Socket s, byte[] buf, int size, out string msg) {
            
            // 存入接收缓冲区的偏移位置
            int offset = 0;
            msg = string.Empty;
            SocketAsyncEventArgs rw = new SocketAsyncEventArgs();
            rw.SetBuffer(buf, 0, buf.Length);

            rw.Completed += (o, e) =>
            {
                IO_Completed(e);
            };
            if (!s.ReceiveAsync(rw))
            {
                IO_Completed(rw);
            }
            
             s.ReceiveAsync(rw);

            return 0;
            while (offset < size) {
                try {
                    int readLen = (size - offset) >= C_MaxReadLen ? C_MaxReadLen : size - offset;
                    int len = s.Receive(buf, offset,readLen, SocketFlags.None);
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
            int size = buf.Length;
            int offset = 0;
            msg = string.Empty;
            while (offset<size) {
                try {
                    int wlen = (size - offset) > C_MaxWriteLen ? C_MaxWriteLen : size - offset;
                    int len = s.Send(buf, offset, wlen, SocketFlags.None);
                    offset += len;
                }catch(Exception e) {
                    msg = $"发送数据时出现异常,异常消息:{e.Message}";
                    return -1;
                }
            }
            return offset;
        }

        /// <summary>
        /// 每次读取数据的最大长度
        /// </summary>
        private const int C_MaxReadLen = 1024;
        /// <summary>
        /// 每次发送数据的最大长度
        /// </summary>
        private const int C_MaxWriteLen = 1024;
    }
}
