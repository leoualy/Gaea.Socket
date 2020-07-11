using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public interface IReceivedCallback
    {
        void SetReceivedCallback(Action<ReceivedEventArgs> handler);
    }
}
