using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_Server
{
    internal class Room
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
