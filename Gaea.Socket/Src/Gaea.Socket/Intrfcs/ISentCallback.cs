using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public interface ISentCallback
    {
        void SetSentHandler(Action<SendEventArgs> handler);
    }
}
