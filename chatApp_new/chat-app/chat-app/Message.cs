using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Message
    {
        // tiêu đề của tin nhắn.
        public string header { get; set; }

        // tên người gửi tin nhắn.
        public string Sender { get; set; }

        // tên người nhận tin nhắn.
        public string Receiver { get; set; }

        // nội dung của tin nhắn.
        public string message { get; set; }
    }
}
