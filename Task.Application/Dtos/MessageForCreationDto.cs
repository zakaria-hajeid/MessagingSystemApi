using System;
using System.Collections.Generic;

namespace Task.Application.Dtos
{
    public class MessageForCreationDto
    {
        public int SenderId { get; set; }
        public int[] RecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string subject { get; set; }
        public string Content { get; set; }
        public MessageForCreationDto()
        {
            MessageSent = DateTime.Now;
        }
    }
}