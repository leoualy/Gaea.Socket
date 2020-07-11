using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSocket.EventArgs {
    public abstract class EventArgsBase {
        public EventArgsBase(int statusCode = 0, string msg = "") {
            StatusCode = statusCode;
            Msg = msg;
        }

        public int StatusCode { get; set; }
        public string Msg { get; set; }
    }
}
