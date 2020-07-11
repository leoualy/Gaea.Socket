using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public interface IConnectedCallback
    {
        void SetConnectedHandler(Action<ConnectedEventArgs> handler);
    }
}
