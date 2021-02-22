using System;
using System.Collections.Generic;


namespace Task.Application.Dtos
{
    public class UserForDetailsDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
     
        public string name { get; set; }
      
        public string Email { get; set; }
  
        public string Phone { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}