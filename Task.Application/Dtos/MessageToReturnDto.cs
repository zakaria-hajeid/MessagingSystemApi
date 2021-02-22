using System;


namespace Task.Application.Dtos
{
    public class MessageToReturnDto
    {
     
        public int Id { get; set; }
        public int SenderId { get; set; }
   
        public string subject { get; set; }
        public string Content { get; set; }

        public DateTime MessageSent { get; set; }

    }
}