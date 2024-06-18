using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class Message
    {
        public string header { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string message { get; set; }
    }
}
