using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GSocket.Implt {
    internal class ServerAysnc:Base.ServerBase {
        protected override void OnAccept() {
            for(int i = 0; i <200; i++)
            {
                SocketAsyncEventArgs eAccept = new SocketAsyncEventArgs();
                eAccept.Completed += EAccept_Completed;
                if (!m_ServerSocket.AcceptAsync(eAccept))
                {
                    ProcessAccept(eAccept);
                }
            }
        }

        private void EAccept_Completed(object sender, SocketAsyncEventArgs e) {
            ProcessAccept(e);
        }


        private void ProcessAccept(SocketAsyncEventArgs e) {
            Socket s = e.AcceptSocket;
            Task.Factory.StartNew(() => {
                if (m_actConnectedHandler != null)
                {
                    m_actConnectedHandler(new ConnectedEventArgs(GetConnection(e.AcceptSocket)));
                }
            });
            e.AcceptSocket = null;
            m_ServerSocket.AcceptAsync(e);
        }
    }
}
