using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public class ErrorEventArgs: EventArgs.EventArgsBase {
        public ErrorEventArgs(int statusCode,string msg) : base(statusCode, msg) { }
    }
}
