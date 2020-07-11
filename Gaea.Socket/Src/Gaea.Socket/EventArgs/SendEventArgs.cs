using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket
{
    public class SendEventArgs: EventArgs.EventArgsBase {
        public SendEventArgs(int statusCode, string msg) : base(statusCode, msg) {
            Length =0;
        }
        public SendEventArgs(int len) : base() {
            Length = len;
        }
        public int Length { get; set; }
    }
}
