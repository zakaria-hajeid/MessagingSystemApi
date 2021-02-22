using System;
using System.Collections.Generic;

namespace Task.Percestance.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public string subject { get; set; }
        public string Content { get; set; }

        public DateTime MessageSent { get; set; }
       
        public ICollection<MessagesReceived> messagesReceivedcs { get; set; }

    }
}