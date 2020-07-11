using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public interface IErrorCallback {
        void SetErrorHandler(Action<ErrorEventArgs> handler);
    }
}
