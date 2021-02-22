using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Application.Dtos
{
    public class UserForUpdateDto
    {
        public string UserName { get; set; }

        public string name { get; set; }

        public string Email { get; set; }
        public Boolean isActive { get; set; }
        public string Phone { get; set; }
        public UserForUpdateDto()
        {
            isActive = true;
        }
    }
   
}
