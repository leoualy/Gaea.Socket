using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public class ReceivedEventArgs:EventArgs.EventArgsBase {
        public ReceivedEventArgs(int statusCode,string msg) : base(statusCode, msg) {
            Buff = null;
        }
        public ReceivedEventArgs(byte[] buf) : base() {
            Buff = buf;
        }
        public byte[] Buff { get; set; }
        
    }
}
