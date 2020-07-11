using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket.Base
{
    internal abstract class AbsCallback
    {
        public void SetConnectedHandler(Action<ConnectedEventArgs> handler)
        {
            m_actConnectedHandler = handler;
        }
        public void SetReceivedCallback(Action<ReceivedEventArgs> handler)
        {
            m_actReceivedHandler = handler;
        }
        public void SetSentHandler(Action<SendEventArgs> handler)
        {
            m_actSentHandler = handler;
        }
        public void SetErrorHandler(Action<ErrorEventArgs> handler) {
            m_actErrorHandler = handler;
        }
        
        protected Action<ConnectedEventArgs> m_actConnectedHandler;
        protected Action<SendEventArgs> m_actSentHandler;
        protected Action<ReceivedEventArgs> m_actReceivedHandler;
        protected Action<ErrorEventArgs> m_actErrorHandler;
    }
}
