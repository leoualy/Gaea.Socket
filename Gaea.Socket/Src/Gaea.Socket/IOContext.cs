using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GSocket
{
    internal class IOContext
    {
        internal int Size { get; set; } = 0;
        internal int OffsetRecved { get; set; } = 0;
        internal int CountRecved { get; set; } = 0;

    }
}
