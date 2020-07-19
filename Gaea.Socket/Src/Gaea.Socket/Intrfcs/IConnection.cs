using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public interface IConnection :ISentCallback,IReceivedCallback,IErrorCallback {
        void Receive(int size,Action<byte[]> callback);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buf">不包含数据段头的内容字节序列</param>
        /// <param name="callback"></param>
        void Send(byte[] buf);
        string GetSourceHost();
        int GetSourcePort();
    }
}
