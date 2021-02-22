using System;
using System.Collections.Generic;
using System.Text;
using Task.percestance.Models;

namespace Task.Percestance.Models
{
    public class MessagesReceived
    {
        public int Id  { get; set; }
        public string subject { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public bool inTrash { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        public int MessageId { get; set; }
        public Message message { get; set; }
      
    }
}
