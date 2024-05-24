using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AP_Server
{
    public class User
    {
        public string UserName { get; set; }

        public TcpClient Client { get; set; }
    }
}
