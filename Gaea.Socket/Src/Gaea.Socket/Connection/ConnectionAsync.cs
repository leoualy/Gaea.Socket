using GSocket.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Microsoft.Win32.SafeHandles;

namespace GSocket.Connection
{
    internal class ConnectionAsync : ConnectionBase
    {
        internal ConnectionAsync(Socket s) : base(s) { }

        SocketAsyncEventArgs eRcv = null;

        public override void Receive(int size,Action<byte[]> callback)
        {
            IOContext ctx = null;
            mRecvCallbak = callback;
            if (eRcv == null)
            {
                ctx = new IOContext();
                eRcv = new SocketAsyncEventArgs();
                eRcv.UserToken = ctx;
                eRcv.Completed += (o, rw) =>
                {
                    IO_Completed(rw);
                };
            }
            ctx = eRcv.UserToken as IOContext;
            ctx.Size = size;
            ctx.OffsetRecved = 0;
            ctx.CountRecved = 0;

            PostRecv();
        }

        public override void Send(byte[] buf)
        {
            PostSend(null);
        }

        private void IO_Completed(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                switch(e.LastOperation)
                {
                    case SocketAsyncOperation.Receive:RecvHandler(e);break;
                    default:break;
                }
            }
        }

        private void PostRecv()
        {
            IOContext ctx = eRcv.UserToken as IOContext;
            if (ctx.CountRecved == 0)
            {
                eRcv.SetBuffer(new byte[ctx.Size], 0, ctx.Size);
            }
            else
            {
                eRcv.SetBuffer(ctx.OffsetRecved, ctx.Size - ctx.CountRecved);
            }

            if (!m_Socket.ReceiveAsync(eRcv))
            {
                RecvHandler(eRcv);
            }
        }

        private Action<byte[]> mRecvCallbak = null;
        private void RecvHandler(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success&&e.BytesTransferred>0)
            {
                IOContext ctx = e.UserToken as IOContext;
                ctx.CountRecved += e.BytesTransferred;
                ctx.OffsetRecved += e.BytesTransferred;

                if (ctx.CountRecved < ctx.Size)
                {
                    PostRecv();
                    return;
                }
                if (mRecvCallbak != null)
                {
                    mRecvCallbak(e.Buffer.Take(e.BytesTransferred).ToArray());
                    return;
                }
            }
        }


        private void PostSend(SocketAsyncEventArgs e)
        {

        }
    }
}
